using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/movies/{movieId}/[controller]")]
public class ReviewsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(int movieId)
    {
        return Created($"api/movies/{movieId}/reviews", null);
    }

    [HttpGet]
    public IActionResult GetAll(int movieId)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int movieId, int id)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int movieId, int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int movieId, int id)
    {
        return NoContent();
    }
}
