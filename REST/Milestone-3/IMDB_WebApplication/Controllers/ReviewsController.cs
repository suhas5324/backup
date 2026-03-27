using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] ReviewRequest request)
        {
            var review = reviewService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = review.Id }, review);
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(reviewService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get( [FromRoute] int id)
        {
            var review = reviewService.Get( id);
            if (review == null)
            {
                return NotFound("Review not found");
            }

            return Ok(review);
        }

        [HttpPut("{id}")]
        public IActionResult Update( [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            var updatedReview = reviewService.Update(id, request);
            if (updatedReview == null)
            {
                return NotFound("Review not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete( [FromRoute] int id)
        {
            var deletedReview = reviewService.Delete(id);
            if (deletedReview == null)
            {
                return NotFound("Review not found");
            }

            return NoContent();
        }
    }
}
