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
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProducerRequest request)
        {
            var producer = _producerService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = producer.Id }, producer);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_producerService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_producerService.Get(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            _producerService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _producerService.Delete(id);
            return NoContent();
        }
    }
}
