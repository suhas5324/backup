using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var genre = genreService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(genreService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(genreService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GenreRequest request)
        {
            genreService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            genreService.Delete(id);
            return NoContent();
        }
    }
}
