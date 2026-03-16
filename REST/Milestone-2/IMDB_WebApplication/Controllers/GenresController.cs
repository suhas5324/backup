using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] Genre genre)
    {
        return StatusCode(201);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Genre genre)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
}