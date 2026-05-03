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
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService producerService;

        public ProducersController(IProducerService producerService)
        {
            this.producerService = producerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProducerRequest request)
        {
            try
            {
                var producer = producerService.Create(request);
                return CreatedAtAction(nameof(Get), new { id = producer.Id }, producer);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(producerService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var producer = producerService.Get(id);
                if (producer == null)
                {
                    return NotFound("Producer not found");
                }

                return Ok(producer);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            try
            {
                var updatedProducer = producerService.Update(id, request);
                if (!updatedProducer)
                {
                    return NotFound("Producer not found");
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
                var deletedProducer = producerService.Delete(id);
                if (!deletedProducer)
                {
                    return NotFound("Producer not found");
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
