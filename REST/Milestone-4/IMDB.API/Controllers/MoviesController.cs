using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var movie = movieService.Create(request);
                return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(movieService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var movie = movieService.Get(id);
                if (movie == null)
                {
                    return NotFound("Movie not found");
                }

                return Ok(movie);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] MovieRequest request)
        {
            try
            {
                var updatedMovie = movieService.Update(id, request);
                if (!updatedMovie)
                {
                    return NotFound("Movie not found");
                }

                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var deletedMovie = movieService.Delete(id);
                if (!deletedMovie)
                {
                    return NotFound("Movie not found");
                }

                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
