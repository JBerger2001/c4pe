using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback_API.Models;
using Feedback_API.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_API.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public StatisticsController(FeedbackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<StatisticsResponse> GetStatistics()
        {
            var stats = new StatisticsResponse()
            {
                AmountPlaces = _context.Places.Count(),
                AmountReviews = _context.Reviews.Count(),
                AmountUsers = _context.Users.Count()
            };

            return stats;
        }
    }
}