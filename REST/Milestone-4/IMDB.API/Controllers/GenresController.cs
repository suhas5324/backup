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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreRequest request)
        {
            var genre = await _genreService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = genre.Id }, genre);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _genreService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _genreService.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] GenreRequest request)
        {
            await _genreService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
