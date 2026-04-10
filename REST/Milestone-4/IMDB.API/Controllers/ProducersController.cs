using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var producer = producerService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = producer.Id }, producer);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(producerService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var producer = producerService.Get(id);
            if (producer == null)
            {
                return NotFound("Producer not found");
            }

            return Ok(producer);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            var updatedProducer = producerService.Update(id, request);
            if (updatedProducer == null)
            {
                return NotFound("Producer not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deletedProducer = producerService.Delete(id);
            if (deletedProducer == null)
            {
                return NotFound("Producer not found");
            }

            return NoContent();
        }
    }
}
