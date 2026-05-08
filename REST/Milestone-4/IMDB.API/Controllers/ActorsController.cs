using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMDB_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ActorRequest request)
        {
            var actor = await _actorService.CreateAsync(request);
            return CreatedAtAction(nameof(GetAsync), new { id = actor.Id }, actor);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _actorService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            return Ok(await _actorService.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ActorRequest request)
        {
            await _actorService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _actorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
