using G_Transport.Dtos;
using G_Transport.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto model)
        {
            var response = await _reviewService.CreateAsync(model);
            if (!response.Status)
            {
                return BadRequest(new { response.Message });
            }
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllReviews([FromQuery] PaginationRequest request)
        {
            var response = await _reviewService.GetAllAsync(request);
            if (!response.Status)
            {
                return NotFound(new { response.Message });
            }
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerReviews([FromQuery] PaginationRequest request)
        {
            var response = await _reviewService.GetAllCustomerReviewsAsync(request);
            if (!response.Status)
            {
                return NotFound(new { response.Message });
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var success = await _reviewService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Review not found" });
            }
            return Ok(new { Message = "Review deleted successfully" });
        }
    }
}
