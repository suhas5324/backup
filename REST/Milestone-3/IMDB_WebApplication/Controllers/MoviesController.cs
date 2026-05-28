using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create([FromBody] MovieRequest request)
        {
            var movie = movieService.Create(request);
            if (movie == null)
            {
                return BadRequest("Invalid movie request, producer not found, or no valid actors were provided.");
            }

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int year)
        {
            return Ok(movieService.GetAll(year));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var movie = movieService.Get(id);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MovieRequest request)
        {
            if (movieService.Get(id) == null)
            {
                return NotFound("Movie not found");
            }

            var updatedMovie = movieService.Update(id, request);
            if (updatedMovie == null)
            {
                return BadRequest("Invalid movie request, producer not found, or no valid actors were provided.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedMovie = movieService.Delete(id);
            if (deletedMovie == null)
            {
                return NotFound("Movie not found");
            }

            return NoContent();
        }
    }
}
