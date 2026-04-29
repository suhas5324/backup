using IMDB_WebApplication.Models.Requests;
using IMDB_WebApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IMDB_WebApplication.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("")]
        public IActionResult Create([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            try
            {
                var review = reviewService.Create(movieId, request);
                if (review == null)
                {
                    return NotFound("Movie not found");
                }

                return CreatedAtAction(nameof(Get), new { movieId, id = review.Id }, review);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("")]
        public IActionResult Get([FromRoute] int movieId)
        {
            try
            {
                var reviews = reviewService.Get(movieId);
                if (reviews == null)
                {
                    return NotFound("Movie not found");
                }

                return Ok(reviews);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int movieId, [FromRoute] int id)
        {
            try
            {
                var review = reviewService.Get(movieId, id);
                if (review == null)
                {
                    return NotFound("Movie or review not found");
                }

                return Ok(review);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int movieId, [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            try
            {
                var updatedReview = reviewService.Update(movieId, id, request);
                if (updatedReview == null)
                {
                    return NotFound("Movie or review not found");
                }

                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            try
            {
                var deletedReview = reviewService.Delete(movieId, id);
                if (deletedReview == null)
                {
                    return NotFound("Movie or review not found");
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
