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
        private readonly IActorService actorService;

        public ActorsController(IActorService actorService)
        {
            this.actorService = actorService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ActorRequest request)
        {
            var actor = actorService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(actorService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {

            var actor = actorService.Get(id);
            if (actor == null)
            {
                return NotFound("Actor not found");
            }

            return Ok(actor);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody] ActorRequest request)
        {

            var updatedActor = actorService.Update(id, request);
            if (updatedActor == null)
            {
                return NotFound("Actor not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deletedActor = actorService.Delete(id);
            if (deletedActor == null)
            {
                return NotFound("Actor not found");
            }

            return NoContent();
        }
    }
}
