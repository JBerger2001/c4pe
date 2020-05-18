using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Feedback_API.Models;
using AutoMapper;
using Feedback_API.Models.Responses;
using Feedback_API.Models.Requests;
using Feedback_API.Models.Domain;
using Microsoft.AspNetCore.Cors;
using Feedback_API.Parameters;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Feedback_API.Services;
using Feedback_API.Extensions;
using AutoMapper.QueryableExtensions;

namespace Feedback_API.Controllers
{
    [Route("api/places")]
    [Authorize]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly FeedbackContext _context;
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;

        private long CurrentUserId => Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        private bool IsAdmin => _context.Users.Find(CurrentUserId).Role == Role.Admin;
        private bool IsAuthorized(long placeId) => HasJWT && (IsPlaceOwner(placeId) || IsAdmin);
        private bool HasJWT => User.Claims.Any();

        public PlacesController(FeedbackContext context, IMapper mapper, IImageUploadService imageUploadService)
        {
            _context = context;
            _mapper = mapper;
            _imageUploadService = imageUploadService;
        }

        #region PLACES
        // GET: api/Places
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceResponse>>> GetPlaces([FromQuery]PlacesParameters parameters)
        {
            var places = await _context.Places
                            .Include(p => p.PlaceType)
                            .Include(p => p.OpeningTimes)
                            .Include(p => p.Reviews)
                            .Include(p => p.Owners)
                            .Include(p => p.Images)
                            .ToListAsync();

            var placesFiltered = places.Where(p => MatchesFilter(p, parameters));

            var placeResponses = _mapper.Map<List<PlaceResponse>>(placesFiltered);

            foreach (var place in placeResponses)
            {
                place.UserIsOwner = IsAuthorized(place.ID);
            }

            var placesSorted = placeResponses.Sort(parameters.OrderBy);

            var placesPaged = PagedList<PlaceResponse>.ToPagedList(placesSorted, parameters.PageNumber, parameters.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(placesPaged.Metadata));

            return placesPaged;
        }

        // GET: api/Places/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceResponse>> GetPlace(long id)
        {
            var place = await _context.Places
                            .Include(p => p.PlaceType)
                            .Include(p => p.OpeningTimes)
                            .Include(p => p.Reviews)
                            .Include(p => p.Owners)
                            .Include(p => p.Images)
                            .FirstOrDefaultAsync(i => i.ID == id);

            if (place == null)
            {
                return NotFound();
            }

            var placeResponse = _mapper.Map<PlaceResponse>(place);
            placeResponse.UserIsOwner = IsAuthorized(placeResponse.ID);

            return placeResponse;
        }

        // PUT: api/Places/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(long id, PlaceRequest placeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*
            var place = _mapper.Map<Place>(placeDTO);
            place.PlaceType = await _context.PlaceTypes.FirstOrDefaultAsync(pt => pt.Name == placeDTO.Type);
            place.PlaceTypeID = place.PlaceType?.ID ?? 0;

            if (place.PlaceType == null)
            {
                return BadRequest();
            }

            */
            if (!IsAuthorized(id))
            {
                return Unauthorized();
            }

            var place = await _context.Places
                            .Include(p => p.OpeningTimes)
                            .Include(p => p.PlaceType)
                            .FirstOrDefaultAsync(p => p.ID == id);
            _mapper.Map(placeRequest, place);
            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Places
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<PlaceResponse>> PostPlace(PlaceRequest placeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var id = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var place = _mapper.Map<Place>(placeRequest);

            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            var placeOwner = new PlaceOwner
            {
                PlaceID = place.ID,
                UserID = CurrentUserId
            };

            _context.PlaceOwners.Add(placeOwner);
            await _context.SaveChangesAsync();

            place = await _context.Places
                        .Include(p => p.OpeningTimes)
                        .Include(p => p.PlaceType)
                        .Include(p => p.Owners)
                        .Include(p => p.Images)
                        .FirstOrDefaultAsync(p => p.ID == place.ID);

            var placeResponse = _mapper.Map<PlaceResponse>(place);

            return CreatedAtAction(nameof(GetPlace), new { id = place.ID }, placeResponse);
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(long id)
        {
            if (!IsAuthorized(id))
            {
                return Unauthorized();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceExists(long id)
        {
            return _context.Places.Any(e => e.ID == id);
        }

        private bool IsPlaceOwner(long placeId)
        {
            return _context.PlaceOwners.Any(po => po.UserID == CurrentUserId && po.PlaceID == placeId);
        }

        private bool MatchesFilter(Place p, PlacesParameters parameters)
        {
            bool matchingCity = string.IsNullOrEmpty(parameters.City)
                ? true
                : p.City.Equals(parameters.City, StringComparison.OrdinalIgnoreCase);

            if (!matchingCity) return false;

            bool matchingCountry = string.IsNullOrEmpty(parameters.Country)
                ? true
                : p.Country.Equals(parameters.Country, StringComparison.OrdinalIgnoreCase);

            if (!matchingCountry) return false;

            bool matchingVerified = parameters.IsVerified.HasValue
                ? p.IsVerified == parameters.IsVerified.Value
                : true;

            if (!matchingVerified) return false;

            bool matchingName = string.IsNullOrEmpty(parameters.Name)
                ? true
                : p.Name.Equals(parameters.Name, StringComparison.OrdinalIgnoreCase);

            if (!matchingName) return false;

            var avgRating = p.Reviews.Count > 0
                ? p.Reviews.Average(r => r.Rating)
                : 0;

            bool matchingMinRating = parameters.MinRating.HasValue
                ? parameters.MinRating <= avgRating
                : true;

            if (!matchingMinRating) return false;

            bool matchingMaxRating = parameters.MaxRating.HasValue
                ? parameters.MaxRating >= avgRating
                : true;

            if (!matchingMaxRating) return false;

            bool matchingPlaceType = parameters.PlaceType.Count > 0
                ? parameters.PlaceType.Contains(p.PlaceType.ID)
                : true;

            if (!matchingPlaceType) return false;

            var now = DateTime.Now.TimeOfDay;

            bool matchingIsOpen = parameters.IsOpen.HasValue
                ? p.OpeningTimes.Any(ot => ot.Open < now && ot.Close > now)
                : true;

            if (!matchingIsOpen) return false;

            return true;
        }


        #endregion

        #region PLACE_OWNER

        [HttpPost("{id}/owner")]
        public async Task<ActionResult<PlaceOwnerFullResponse>> PostPlaceOwner(PlaceOwnerRequest placeOwnerRequest, long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsAuthorized(id))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(placeOwnerRequest.Username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                return NotFound("User not found");
            }

            var placeOwner = new PlaceOwner
            {
                UserID = user.ID,
                PlaceID = id
            };

            _context.PlaceOwners.Add(placeOwner);
            await _context.SaveChangesAsync();

            return _mapper.Map<PlaceOwnerFullResponse>(placeOwner);
        }

        [HttpDelete("{placeId}/owner/{userId}")]
        public async Task<IActionResult> DeletePlaceOwner(long placeId, long userId)
        {
            if (!IsAuthorized(placeId))
            {
                return Unauthorized();
            }

            if (userId == CurrentUserId)
            {
                return BadRequest("You can't remove yourself from the place owners.");
            }

            var placeOwner = await _context.PlaceOwners.FirstOrDefaultAsync(po => po.UserID == userId && po.PlaceID == placeId);
            if (placeOwner == null)
            {
                return NotFound();
            }

            _context.PlaceOwners.Remove(placeOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        #endregion

        #region OPENING_TIMES

        // GET: api/Places/5/openingtimes
        [AllowAnonymous]
        [HttpGet("{id}/OpeningTimes")]
        public async Task<ActionResult<IEnumerable<OpeningTimeResponse>>> GetOpeningTimes([FromQuery]OpeningTimeParameters parameters, long id)
        {
            var place = await _context.Places
                            .Include(p => p.OpeningTimes)
                            .FirstOrDefaultAsync(p => p.ID == id);

            if (place == null)
            {
                return NotFound();
            }

            //var openingTimes = place.OpeningTimes.Where(ot => ot.Day == parameters.Day);
            var openingTimes = place.OpeningTimes;

            if (parameters.Day.HasValue)
            {
                openingTimes = openingTimes.Where(ot => ot.Day == parameters.Day.Value).ToList();
            }
            var openingTimesPaged = PagedList<OpeningTime>.ToPagedList(openingTimes, parameters.PageNumber, parameters.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(openingTimesPaged.Metadata));

            return _mapper.Map<List<OpeningTimeResponse>>(openingTimesPaged);
        }

        // POST: api/Places/5/OpeningTimes
        [HttpPost("{placeId}/OpeningTimes")]
        public async Task<ActionResult<OpeningTimeResponse>> PostOpeningTime(long placeId, OpeningTimeRequest openingTimeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsAuthorized(placeId))
            {
                return Unauthorized();
            }

            if (!IsValidRange(placeId, openingTimeRequest))
            {
                return BadRequest("Opening time overlaps with an existing one.");
            }

            var openingTime = _mapper.Map<OpeningTime>(openingTimeRequest);
            openingTime.PlaceID = placeId;
            _context.OpeningTimes.Add(openingTime);
            await _context.SaveChangesAsync();

            var placeResponse = _mapper.Map<OpeningTimeResponse>(openingTime);

            return CreatedAtAction(nameof(GetPlace), new { id = openingTime.ID }, placeResponse);
        }

        // PUT: api/Places/5/OpeningTimes
        [HttpPut("{placeId}/OpeningTimes/{openingTimeId}")]
        public async Task<ActionResult<OpeningTimeResponse>> PutOpeningTime(long placeId, long openingTimeId, OpeningTimeRequest openingTimeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!IsAuthorized(placeId))
            {
                return Unauthorized();
            }

            if (!IsValidRange(placeId, openingTimeRequest, openingTimeId))
            {
                return BadRequest("Opening time overlaps with an existing one.");
            }

            var openingTime = _mapper.Map<OpeningTime>(openingTimeRequest);
            openingTime.ID = openingTimeId;
            openingTime.PlaceID = placeId;
            _context.Entry(openingTime).State = EntityState.Modified;

            var placeResponse = _mapper.Map<OpeningTimeResponse>(openingTime);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpeningTimeExists(placeId, openingTimeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{placeId}/OpeningTimes/{openingTimeId}")]

        public async Task<IActionResult> DeleteOpeningTime(long placeId, long openingTimeId)
        {
            if (!IsAuthorized(placeId))
            {
                return Unauthorized();
            }

            var place = await _context.Places.FindAsync(placeId);
            var openingTime = await _context.OpeningTimes.FindAsync(openingTimeId);
            if (place == null || openingTime == null)
            {
                return NotFound();
            }

            _context.OpeningTimes.Remove(openingTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool OpeningTimeExists(long placeId, long openingTimeId)
        {
            return _context.OpeningTimes.Any(o => o.ID == openingTimeId && o.PlaceID == placeId);
        }

        private bool IsValidRange(long placeId, OpeningTimeRequest ot, long? openingTimeId = null)
        {
            return !_context.OpeningTimes.Any(o =>
                o.PlaceID == placeId
                && ((openingTimeId.HasValue) ? o.ID != openingTimeId.Value : true)
                && o.Day == ot.Day
                && o.Open < ot.Close
                && o.Close > ot.Open);
        }
        #endregion

        #region REVIEWS
        // GET: api/Places/5/Reviews
        [AllowAnonymous]
        [HttpGet("{id}/Reviews")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviews([FromQuery]ReviewParameters parameters, long id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            var reviews = _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Reactions)
                .Where(r => r.PlaceID == id)
                .Where(r => r.Rating >= parameters.MinRating)
                .Where(r => r.Rating <= parameters.MaxRating);

            var pagedReviews = PagedList<Review>.ToPagedList(reviews, parameters.PageNumber, parameters.PageSize);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedReviews.Metadata));

            return _mapper.Map<List<ReviewResponse>>(reviews);
        }

        // GET: api/Places/5/Reviews/1
        [AllowAnonymous]
        [HttpGet("{placeId}/Reviews/{reviewId}")]
        public async Task<ActionResult<ReviewResponse>> GetReview(long placeId, long reviewId)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Reactions)
                .FirstOrDefaultAsync(r => r.ID == reviewId && r.PlaceID == placeId);

            if (review == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReviewResponse>(review);
        }


        // POST: api/Places/5/Reviews
        [HttpPost("{placeId}/Reviews")]
        public async Task<ActionResult<ReviewResponse>> PostReview(long placeId, ReviewRequest reviewRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAlreadyReviewed = await _context.Reviews.AnyAsync(r => r.PlaceID == placeId && r.UserID == CurrentUserId);
            if (userAlreadyReviewed)
            {
                return BadRequest("This user already reviewed this place");
            }

            var review = _mapper.Map<Review>(reviewRequest);
            review.PlaceID = placeId;
            review.UserID = CurrentUserId;
            review.Time = DateTime.Now;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            review = await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ID == review.ID);

            var reviewResponse = _mapper.Map<ReviewResponse>(review);

            return CreatedAtAction(nameof(GetReview), new { placeId, reviewId = review.ID }, reviewResponse);
        }

        [HttpPut("{placeId}/Reviews/{reviewId}")]
        public async Task<ActionResult<ReviewResponse>> PutReview(long placeId, long reviewId, ReviewRequest reviewRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = _context.Reviews.FirstOrDefault(r => r.PlaceID == placeId && r.ID == reviewId);

            if (review == null)
            {
                return NotFound();
            }

            if ((review.UserID != CurrentUserId) && !IsAdmin)
            {
                return Unauthorized();
            }

            _mapper.Map(reviewRequest, review);

            review.LastEdited = DateTime.Now;

            _context.Entry(review).State = EntityState.Modified;

            var reviewResponse = _mapper.Map<ReviewResponse>(review);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(reviewId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/Reviews/5/Reviews
        [HttpDelete("{placeId}/Reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(long placeId, long reviewId)
        {
            var place = await _context.Places.FindAsync(placeId);
            var review = await _context.Reviews.FindAsync(reviewId);
            if (place == null || review == null)
            {
                return NotFound();
            }

            if ((review.UserID != CurrentUserId) && !IsAdmin)
            {
                return Unauthorized();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(long id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
        #endregion

        #region REACTION
        [HttpGet("{placeId}/reviews/{reviewId}/reaction")]
        public async Task<ActionResult<ReactionResponse>> GetReaction(long placeId, long reviewId)
        {
            var reaction = await _context.Reactions.FindAsync(reviewId, CurrentUserId);
            if (reaction == null)
            {
                return NotFound();
            }

            return _mapper.Map<ReactionResponse>(reaction);
        }

        [HttpPost("{placeId}/reviews/{reviewId}/reaction")]
        public async Task<ActionResult<ReactionResponse>> PostReaction(long placeId, long reviewId, ReactionRequest reactionRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var place = _context.Places.Find(placeId);
            var review = _context.Reviews.Find(reviewId);

            if (place == null || review == null)
            {
                return NotFound();
            }

            //var userAlreadyReacted = await _context.Reactions.AnyAsync(r => r.ReviewID == reviewId && r.UserID == CurrentUserId);
            var reaction = await _context.Reactions.FindAsync(reviewId, CurrentUserId);
            if (reaction != null)
            {
                //var reaction = _context.Reactions.Where(r => r.ReviewID == reviewId && r.UserID == CurrentUserId);
                _mapper.Map(reactionRequest, reaction);

                _context.Entry(reaction).State = EntityState.Modified;
            }
            else
            {
                reaction = _mapper.Map<Reaction>(reactionRequest);

                reaction.UserID = CurrentUserId;
                reaction.ReviewID = reviewId;

                _context.Reactions.Add(reaction);
            }

            await _context.SaveChangesAsync();


            return _mapper.Map<ReactionResponse>(reaction);
        }

        [HttpDelete("{placeId}/reviews/{reviewId}/reaction")]
        public async Task<ActionResult> DeleteReaction(long placeId, long reviewId)
        {
            var reaction = await _context.Reactions.FindAsync(reviewId, CurrentUserId);

            if (reaction == null)
            {
                return NotFound();
            }

            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{placeId}/reviews/{reviewId}/reaction/{userId}")]
        public async Task<ActionResult> DeleteReactionById(long placeId, long reviewId, long userId)
        {
            var reaction = await _context.Reactions.FindAsync(reviewId, userId);

            if (reaction == null)
            {
                return NotFound();
            }

            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region IMAGES

        [HttpPost("{placeId}/images/{imageId}")]
        public async Task<ActionResult<string>> PostUploadImage(long placeId, int imageId, IFormFile file)
        {
            if (imageId < 1 || imageId > 3)
            {
                return BadRequest("You can only upload 3 images per place.");
            }

            if (!IsAuthorized(placeId))
            {
                return Unauthorized();
            }

            if(!_imageUploadService.IsValid(file))
            {
                return BadRequest("Invalid image. Maximum Image size is 8MB, supported file types are .jpg, .jpeg and .png");
            }

            var uriPath = await _imageUploadService.SavePlaceImage(file, placeId, imageId);

            return Ok(new { uriPath });
        }

        [HttpDelete("{placeId}/images/{imageId}")]
        public async Task<ActionResult> DeleteImage(long placeId, int imageId)
        {
            var place = await _context.Places
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ID == placeId);

            if (place == null || place?.Images == null)
            {
                return NotFound();
            }

            var image = place.Images.FirstOrDefault(p => p?.ID == imageId);

            if (image == null)
            {
                return NotFound();
            }

            _imageUploadService.RemoveImage(image.URI);

            _context.PlaceImages.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion
    }
}
