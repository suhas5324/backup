using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create([FromBody] ActorRequest request)
        {
            var actor = _actorService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_actorService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_actorService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ActorRequest request)
        {
            _actorService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _actorService.Delete(id);
            return NoContent();
        }
    }
}
