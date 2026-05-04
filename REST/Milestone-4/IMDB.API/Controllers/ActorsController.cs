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
            return Ok(actorService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ActorRequest request)
        {
            actorService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            actorService.Delete(id);
            return NoContent();
        }
    }
}
