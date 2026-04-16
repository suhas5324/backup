using System.ComponentModel.DataAnnotations;
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
        public IActionResult Create([FromForm] MovieRequest request)
        {
            var movie = movieService.Create(request);
            if (movie == null)
            {
                return BadRequest("Invalid movie payload");
            }

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movieService.Get());
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
        public IActionResult Update([FromRoute] int id, [FromForm] MovieRequest request)
        {
            var updatedMovie = movieService.Update(id, request);
            if (updatedMovie == null)
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.ProducerId <= 0 || id <= 0)
                {
                    return BadRequest("Invalid movie payload");
                }

                return NotFound("Movie not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
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
