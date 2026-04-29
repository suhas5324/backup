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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreRequest request)
        {
            try
            {
                var genre = genreService.Create(request);
                return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(genreService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var genre = genreService.Get(id);
                if (genre == null)
                {
                    return NotFound("Genre not found");
                }

                return Ok(genre);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GenreRequest request)
        {
            try
            {
                var updatedGenre = genreService.Update(id, request);
                if (updatedGenre == null)
                {
                    return NotFound("Genre not found");
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
                var deletedGenre = genreService.Delete(id);
                if (deletedGenre == null)
                {
                    return NotFound("Genre not found");
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
