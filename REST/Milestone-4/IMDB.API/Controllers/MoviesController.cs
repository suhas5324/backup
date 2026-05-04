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
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieRequest request)
        {
            var movie = await movieService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movieService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(movieService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] MovieRequest request)
        {
            await movieService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await movieService.Delete(id);
            return NoContent();
        }
    }
}
