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
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("")]
        public IActionResult Create([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            var review = _reviewService.Create(movieId, request);
            return CreatedAtAction(nameof(Get), new { movieId, id = review.Id }, review);
        }

        [HttpGet("")]
        public IActionResult Get([FromRoute] int movieId)
        {
            return Ok(_reviewService.Get(movieId));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int movieId, [FromRoute] int id)
        {
            return Ok(_reviewService.Get(movieId, id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int movieId, [FromBody] ReviewRequest request, [FromRoute] int id)
        {
            _reviewService.Update(movieId, id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            _reviewService.Delete(movieId, id);
            return NoContent();
        }
    }
}
