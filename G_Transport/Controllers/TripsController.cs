using Microsoft.AspNetCore.Mvc;
using G_Transport.Services.Interfaces;
using G_Transport.Dtos;
using Microsoft.AspNetCore.Authorization;
using G_Transport.Services.Implementations;
using System.Threading.Tasks;
using G_Transport.Models.Enums;

namespace G_Transport.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly ICurrentUser _currentUser;

        public TripController(ITripService tripService, ICurrentUser currentUser)
        {
            _tripService = tripService;
            _currentUser = currentUser;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripRequestModel model)
        {
            var result = await _tripService.CreateAsync(model);
            if (!result.Status)
                return BadRequest(result);
            return Ok(result);
        }

        //[Authorize(Roles = "Customer,Driver,Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTrips([FromQuery] PaginationRequest request)
        {
            var result = await _tripService.GetAllAsync(request);
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(Guid id)
        {
            var result = await _tripService.GetAsync(t => t.Id == id);
            if (!result.Status)
                return NotFound(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTrip([FromBody] UpdateTripRequestModel model)
        {
            var result = await _tripService.UpdateAsync(model);
            if (!result.Status)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("customer")]
        [HttpGet("without-review")]
        public async Task<IActionResult> GetTripsWithoutReview([FromQuery] PaginationRequest request)
        {
            var response = await _tripService.GetAllWithoutReviewAsync(request);

            if (!response.Status)
            {
                return NotFound(new { response.Message });
            }

            return Ok(response);
        }
        [HttpGet("recent")]
        [Authorize] // Ensures only authenticated users can access this endpoint
        public async Task<IActionResult> GetRecentTrips([FromQuery] PaginationRequest request)
        {
            var response = await _tripService.GetAllRecent(request);
            if (!response.Status)
            {
                return NotFound(response); // Returns 404 if no trips are found
            }
            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            var success = await _tripService.DeleteAsync(id);
            if (!success)
                return NotFound(new { Status = false, Message = "Trip not found or already deleted." });
            return Ok(new { Status = true, Message = "Trip deleted successfully." });
        }
        //[Authorize(Roles = "Customer,Admin")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTrips([FromQuery] PaginationRequest request)
        {
            var response = await _tripService.GetAllAvailableAsync(request);

            if (!response.Status)
                return NotFound(new { message = response.Message });

            return Ok(response);
        }

        [HttpGet("assigned-trip")]
        public async Task<IActionResult> GetAssignedTrip()
        {
            var response = await _tripService.GetDriverAssigned();

            if (!response.Status)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpGet("TripCount")]
        public IActionResult SuccessfullTripCount()
        {
            var response = _tripService.TripCount(x => x.Status == Status.Successful);

            return Ok(new { SuccessfullTripCount = response });
        }
        [HttpGet("FailedTripCount")]
        public IActionResult FailedtripCount()
        {
            var response = _tripService.TripCount(x => x.Status == Status.Failed);

            return Ok(new { FailedTripCount = response });
        }
        [HttpGet("Customer-Trips-Count")]
        public IActionResult CustomerTripCount()
        {
            var currentUser = _currentUser.GetCurrentuserId();
            var response = _tripService.TripCount(x => x.Bookings.Any(c => c.CustomerId == currentUser) && x.Status == Status.Successful);
            return Ok(new { CustomerTrip = response });

        }
        [HttpGet("Customer-Trips-pending-review")]
        public IActionResult CustomerPendingReviewCount()
        {
            var currentUser = _currentUser.GetCurrentuserId();
            var response = _tripService.TripCount(x => !x.Reviews.Any(r => r.CustomerId == currentUser));
            return Ok(new { PendingReview = response });

        }
        [HttpGet("Customer-Trips")]
        public async Task<IActionResult> CustomerTrips([FromQuery] PaginationRequest request)
        {
            var response = await _tripService.GetAllCustomerTrip(request);
            if (response.Status)
            {
                return Ok(response);
            }
            return NoContent();
        }
        [Authorize]
        [HttpPatch("Status")]
        public async Task<IActionResult> Triggerstatus([FromQuery] TriggerTripStatus model)
        {
            var response = await _tripService.TriggerTripStatus(model);
            if (!response.Status)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

