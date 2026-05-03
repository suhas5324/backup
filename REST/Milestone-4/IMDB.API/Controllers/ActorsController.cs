using IMDB_WebApplication.Models.RequestModels;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var actor = actorService.Create(request);
                return CreatedAtAction(nameof(Get), new { id = actor.Id }, actor);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(actorService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var actor = actorService.Get(id);
                if (actor == null)
                {
                    return NotFound("Actor not found");
                }

                return Ok(actor);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ActorRequest request)
        {
            try
            {
                var updatedActor = actorService.Update(id, request);
                if (!updatedActor)
                {
                    return NotFound("Actor not found");
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
                var deletedActor = actorService.Delete(id);
                if (!deletedActor)
                {
                    return NotFound("Actor not found");
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
