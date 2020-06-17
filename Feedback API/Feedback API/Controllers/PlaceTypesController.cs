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
using Microsoft.AspNetCore.Authorization;

namespace Feedback_API.Controllers
{
    [Route("api/placetypes")]
    [Authorize(Roles = Role.Admin)]
    [ApiController]
    public class PlaceTypesController : ControllerBase
    {
        private readonly FeedbackContext _context;
        private readonly IMapper _mapper;

        public PlaceTypesController(FeedbackContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PlaceTypes
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceTypeResponse>>> GetPlaceTypes()
        {
            var placeTypes = await _context.PlaceTypes.ToListAsync();
            return _mapper.Map<List<PlaceTypeResponse>>(placeTypes);
        }

        // GET: api/PlaceTypes/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceTypeResponse>> GetPlaceType(long id)
        {
            var placeType = await _context.PlaceTypes.FindAsync(id);

            if (placeType == null)
            {
                return NotFound();
            }

            return _mapper.Map<PlaceTypeResponse>(placeType);
        }

        // PUT: api/PlaceTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaceType(long id, PlaceTypeRequest placeTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var placeType = await _context.PlaceTypes.FindAsync(id);
            _mapper.Map(placeTypeDTO, placeType);
            _context.Entry(placeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceTypeExists(id))
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

        // POST: api/PlaceTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PlaceTypeResponse>> PostPlaceType(PlaceTypeRequest placeTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var placeType = _mapper.Map<PlaceType>(placeTypeDTO);
            _context.PlaceTypes.Add(placeType);
            await _context.SaveChangesAsync();

            var placeTypeResponse = _mapper.Map<PlaceTypeResponse>(placeType);

            return CreatedAtAction("GetPlaceType", new { id = placeType.ID }, placeTypeResponse);
        }

        // DELETE: api/PlaceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaceType(long id)
        {
            var placeType = await _context.PlaceTypes.FindAsync(id);
            if (placeType == null)
            {
                return NotFound();
            }

            _context.PlaceTypes.Remove(placeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaceTypeExists(long id)
        {
            return _context.PlaceTypes.Any(e => e.ID == id);
        }
    }
}
