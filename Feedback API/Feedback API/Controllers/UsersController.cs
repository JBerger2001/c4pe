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
using Feedback_API.Models.Domain;
using Microsoft.AspNetCore.Cors;
using Feedback_API.Models.Requests;
using Feedback_API.Services;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Feedback_API.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FeedbackContext _context;
        private readonly IAuthService _authService;
        private readonly IImageUploadService _imageUploadService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private long CurrentUserId => Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        private bool IsAdmin => _context.Users.Find(CurrentUserId).Role == Role.Admin;
        private bool HasJWT => User.Claims.Any();

        public UsersController(FeedbackContext context, IAuthService authService, IImageUploadService imageUploadService, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _imageUploadService = imageUploadService;
            _config = config;
            _mapper = mapper;
        }

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPublicResponse>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Reviews).ToListAsync();
            return _mapper.Map<List<UserPublicResponse>>(users);
        }

        // GET: api/Users/me
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserPrivateResponse>> GetCurrentUser()
        {
            var user = await _context.Users.FindAsync(CurrentUserId);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            await _context.Entry(user).Collection(u => u.Reviews).LoadAsync();

            return _mapper.Map<UserPrivateResponse>(user);
        }

        // GET: api/Users/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPublicResponse>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            await _context.Entry(user).Collection(u => u.Reviews).LoadAsync();

            return _mapper.Map<UserPublicResponse>(user);
        }

        private void AddUserReactionIsHelpful(IEnumerable<ReviewUserResponse> reviewUserResponses)
        {
            if (HasJWT)
            {
                foreach (var review in reviewUserResponses)
                {
                    var userReaction = _context.Reactions.Find(review.ID, CurrentUserId);

                    review.UserReactionIsHelpful = userReaction?.IsHelpful;
                }
            }

        }

        [AllowAnonymous]
        [HttpGet("{id}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewUserResponse>>> GetUserReviews(long id)
        {
            List<Review> reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Place)
                .Include(r => r.Place.OpeningTimes)
                .Include(r => r.Place.PlaceType)
                .Include(r => r.Reactions)
                .Where(r => r.UserID == id).ToListAsync();

            var response = _mapper.Map<List<ReviewUserResponse>>(reviews);
            AddUserReactionIsHelpful(response);

            return response;
        }

        [HttpGet("me/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewUserResponse>>> GetCurrentUserReviews()
        {
            List<Review> reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Place)
                .Include(r => r.Place.OpeningTimes)
                .Include(r => r.Place.PlaceType)
                .Include(r => r.Reactions)
                .Where(r => r.UserID == CurrentUserId).ToListAsync();

            var response = _mapper.Map<List<ReviewUserResponse>>(reviews);
            AddUserReactionIsHelpful(response);

            return response;
        }

        // PUT: api/Users/5
        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, UserFullUpdateRequest userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userDTO.Role != Role.Admin && userDTO.Role != Role.Admin)
            {
                return BadRequest("Invalid role.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            _mapper.Map(userDTO, user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/me
        [HttpPut("me")]
        public async Task<IActionResult> PutCurrentUser(UserUpdateRequest userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == CurrentUserId);
            _mapper.Map(userDTO, user);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(CurrentUserId))
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

        // POST api/users/me/avatar
        [HttpPost("me/avatar")]
        public async Task<IActionResult> PostUploadAvatar(IFormFile image)
        {
            if (!_imageUploadService.IsValid(image))
            {
                return BadRequest(ImageUploadService.INVALID_MESSAGE);
            }

            var uriPath = await _imageUploadService.SaveAvatar(image, CurrentUserId);

            return Ok(new { uriPath });
        }

        // POST: api/Users/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid ModelState");
            }

            userRegisterRequest.Username = userRegisterRequest.Username;

            if (await _authService.UserExists(userRegisterRequest.Username))
            {
                return BadRequest("Username is already taken");
            }

            var newUser = _mapper.Map<User>(userRegisterRequest);
            newUser.Role = Role.User;

            await _authService.Register(newUser, userRegisterRequest.Password);

            return StatusCode(201);
        }

        // POST: api/Users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            var user = await _authService.Login(userLoginRequest.Username.ToLower(), userLoginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString, userId = user.ID });
        }

        // DELETE: api/Users/5
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/me
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var user = await _context.Users.FindAsync(CurrentUserId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/me/avatar
        [HttpDelete("me/avatar")]
        public async Task<IActionResult> DeleteCurrentUserAvatar()
        {
            var user = await _context.Users.FindAsync(CurrentUserId);
            if (user == null || user?.AvatarURI == null)
            {
                return NotFound();
            }

            _imageUploadService.RemoveImage(user.AvatarURI);

            user.AvatarURI = null;

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
