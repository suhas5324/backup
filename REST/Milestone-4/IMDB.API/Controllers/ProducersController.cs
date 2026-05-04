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
            return Ok(producerService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            producerService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            producerService.Delete(id);
            return NoContent();
        }
    }
}
