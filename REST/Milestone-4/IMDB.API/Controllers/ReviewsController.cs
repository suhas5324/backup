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
            var review = reviewService.Create(movieId, request);
            return CreatedAtAction(nameof(Get), new { movieId, id = review.Id }, review);
        }

        [HttpGet("")]
        public IActionResult Get([FromRoute] int movieId)
        {
            return Ok(reviewService.Get(movieId));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int movieId, [FromRoute] int id)
        {
            return Ok(reviewService.Get(movieId, id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int movieId, [FromBody] ReviewRequest request, [FromRoute] int id)
        {
            reviewService.Update(movieId, id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            reviewService.Delete(movieId, id);
            return NoContent();
        }
    }
}
