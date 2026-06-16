using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieRequest request)
        {
            var movie = await _movieService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _movieService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _movieService.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] MovieRequest request)
        {
            await _movieService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _movieService.DeleteAsync(id);
            return NoContent();
        }
    }
}
