using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateAsync([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            var review = await _reviewService.CreateAsync(movieId, request);
            return CreatedAtAction(nameof(Get), new { movieId, id = review.Id }, review);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync([FromRoute] int movieId)
        {
            return Ok(await _reviewService.GetAsync(movieId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int movieId, [FromRoute] int id)
        {
            return Ok(await _reviewService.GetAsync(movieId, id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int movieId, [FromBody] ReviewRequest request, [FromRoute] int id)
        {
            await _reviewService.UpdateAsync(movieId, id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int movieId, [FromRoute] int id)
        {
            await _reviewService.DeleteAsync(movieId, id);
            return NoContent();
        }
    }
}
