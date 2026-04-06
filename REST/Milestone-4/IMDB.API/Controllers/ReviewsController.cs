using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("")]
        public IActionResult Create([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Invalid review request.");
            }

            var review = reviewService.Create(movieId, request);
            if (review == null)
            {
                return NotFound("Movie not found");
            }

            return CreatedAtAction(nameof(Get), new { movieId, id = review.Id }, review);
        }

        [HttpGet("")]
        public IActionResult Get([FromRoute] int movieId)
        {
            var reviews = reviewService.Get(movieId);
            if (reviews == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int movieId, [FromRoute] int id)
        {
            var review = reviewService.Get(movieId, id);
            if (review == null)
            {
                return NotFound("Movie or review not found");
            }

            return Ok(review);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int movieId, [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Invalid review request.");
            }

            var updatedReview = reviewService.Update(movieId, id, request);
            if (updatedReview == null)
            {
                return NotFound("Movie or review not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            var deletedReview = reviewService.Delete(movieId, id);
            if (deletedReview == null)
            {
                return NotFound("Movie or review not found");
            }

            return NoContent();
        }
    }
}
