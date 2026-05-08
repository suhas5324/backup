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
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProducerRequest request)
        {
            var producer = await _producerService.CreateAsync(request);
            return CreatedAtAction(nameof(GetAsync), new { id = producer.Id }, producer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _producerService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            return Ok(await _producerService.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ProducerRequest request)
        {
            await _producerService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _producerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
