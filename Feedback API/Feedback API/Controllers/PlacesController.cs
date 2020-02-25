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

namespace Feedback_API.Controllers
{
    [Route("api/places")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly FeedbackContext _context;
        private readonly IMapper _mapper;

        public PlacesController(FeedbackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region PLACES
        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceResponse>>> GetPlaces()
        {
            var places = await _context.Places
                            .Include(p => p.PlaceType)
                            .Include(p => p.OpeningTimes)
                            .Include(p => p.Reviews)
                            .ToListAsync();

            return _mapper.Map<List<PlaceResponse>>(places);
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceResponse>> GetPlace(long id)
        {
            var place = await _context.Places
                            .Include(p => p.PlaceType)
                            .Include(p => p.OpeningTimes)
                            .Include(p => p.Reviews)
                            .FirstOrDefaultAsync(i => i.ID == id);

            if (place == null)
            {
                return NotFound();
            }

            return _mapper.Map<PlaceResponse>(place);
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
        [HttpPost]
        public async Task<ActionResult<PlaceResponse>> PostPlace(PlaceRequest placeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var place = _mapper.Map<Place>(placeRequest);
            //place.PlaceType = await _context.PlaceTypes.FirstOrDefaultAsync(pt => pt.Name == placeDTO.Type.Name);
            //place.PlaceTypeID = place.PlaceType?.ID ?? 0;
            /*
                        if (place.PlaceType == null)
                        {
                            return BadRequest();
                        }
            */
            _context.Places.Add(place);
            await _context.SaveChangesAsync();

            place = await _context.Places
                        .Include(p => p.OpeningTimes)
                        .Include(p => p.PlaceType)
                        .FirstOrDefaultAsync(p => p.ID == place.ID);

            var placeResponse = _mapper.Map<PlaceResponse>(place);

            return CreatedAtAction(nameof(GetPlace), new { id = place.ID }, placeResponse);
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(long id)
        {
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

  
        #endregion

        #region OPENING_TIMES

        // GET: api/Places/5/openingtimes
        [HttpGet("{id}/OpeningTimes")]
        public async Task<ActionResult<IEnumerable<OpeningTimeResponse>>> GetOpeningTimes(long id)
        {
            var place = await _context.Places
                            .Include(p => p.OpeningTimes)
                            .FirstOrDefaultAsync(p => p.ID == id);

            if (place == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<OpeningTimeResponse>>(place.OpeningTimes);
        }

        // POST: api/Places/5/OpeningTimes
        [HttpPost("{placeId}/OpeningTimes")]
        public async Task<ActionResult<OpeningTimeResponse>> PostOpeningTime(long placeId, OpeningTimeRequest openingTimeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!IsValidRange(placeId, openingTimeRequest))
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

            if(!IsValidRange(placeId, openingTimeRequest, openingTimeId))
            {
                return BadRequest("Opening time overlaps with an existing one.");
            }

            var openingTime = _mapper.Map<OpeningTime>(openingTimeRequest);
            openingTime.ID = openingTimeId;
            openingTime.PlaceID = placeId;
            _context.Entry(openingTime).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
                && (openingTimeId.HasValue) ? o.ID != openingTimeId.Value : true
                && o.Day == ot.Day
                && o.Open < ot.Close
                && o.Close > ot.Open); 
        }
        #endregion

        #region REVIEWS
        // GET: api/Places/5/Reviews
        [HttpGet("{id}/Reviews")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviews(long id)
        {
            var place = await _context.Places
                            .Include(p => p.Reviews)
                            .FirstOrDefaultAsync(p => p.ID == id);

            if (place == null)
            {
                return NotFound();
            }

            var reviews = place.Reviews;
            foreach (var review in reviews)
            {
                review.User = await _context.Users.FindAsync(review.UserID);
            }

            return _mapper.Map<List<ReviewResponse>>(place.Reviews);
        }

        // GET: api/Places/5/Reviews/1
        [HttpGet("{placeId}/Reviews/{reviewId}")]
        public async Task<ActionResult<ReviewResponse>> GetReview(long placeId, long reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ID == reviewId && r.PlaceID == placeId);

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

            var userAlreadyReviewed = await _context.Reviews.AnyAsync(r => r.PlaceID == placeId && r.UserID == reviewRequest.UserID);
            if (userAlreadyReviewed)
            {
                return BadRequest("This user already reviewed this place");
            }

            var review = _mapper.Map<Review>(reviewRequest);
            review.PlaceID = placeId;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

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

            var review = _mapper.Map<OpeningTime>(reviewRequest);
            review.ID = reviewId;
            review.PlaceID = placeId;
            _context.Entry(review).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(long id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
        #endregion
    }
}
