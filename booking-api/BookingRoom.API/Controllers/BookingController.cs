using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.API.Controllers
{
    [ApiController]
    [Route("Booking")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        protected readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("")]
        [Authorize(Roles = "admin")]
        public async Task<IResult> Create([FromBody] CreateBookingRequest createBookingRequest)
        {
            var result = await _bookingService.Create(createBookingRequest);

            if (!result.IsSuccess)
                return Results.Problem(result.Error, statusCode: result.StatusCode);

            return Results.Ok(result.Value);
        }

        [HttpGet("ByUser/{userId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ListAllBookingsByUser(string userId)
        {
            var timeSlots = await _bookingService.ListAllBookingsByUser(userId);
            return Ok(timeSlots);
        }

        [HttpGet("ByBooking/{bookingId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetBookingsByUser(string bookingId)
        {
            var booking = await _bookingService.GetBookingById(bookingId);
            return Ok(booking.Value);
        }

        [HttpDelete("{bookingId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string bookingId)
        {
            var booking = await _bookingService.Delete(bookingId);
            return Ok(booking);
        }

        [HttpPut("")]
        [Authorize(Roles = "admin")]
        public async Task<IResult> Update([FromBody] UpdateBookingRequest updateBookingRequest)
        {
            var result = await _bookingService.Update(updateBookingRequest);

            if (!result.IsSuccess)
                return Results.Problem(result.Error, statusCode: result.StatusCode);

            return Results.Ok(result.Value);

        }

    }
}
