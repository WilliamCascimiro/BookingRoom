using BookingRoom.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingRoom.API.Controllers
{
    [ApiController]
    [Route("Room")]
    [Authorize]
    public class RoomController : ControllerBase
    {
        protected readonly IRoomService _roomService;
        protected readonly IRoomTimeSlotService _roomTimeSlotService;

        public RoomController(IRoomService roomService, IRoomTimeSlotService roomTimeSlotService)
        {
            _roomService = roomService;
            _roomTimeSlotService = roomTimeSlotService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Index()
        {
            var booking = await _roomService.GetAll();
            return Ok(booking);
        }

        [HttpGet("{roomId}/get-time-slots-selected")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetRoomDateTimeSlots(string roomId, [FromQuery] DateTime date, [FromQuery] string bookingId)
        {
            var timeSlots = await _roomTimeSlotService.GetRoomDateTimeSlotsAsync(roomId, date, bookingId);
            return Ok(timeSlots);
        }

        [HttpGet("{roomId}/get-time-slots")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRoomDateTimeSlots(string roomId, [FromQuery] DateTime date)
        {
            var timeSlots = await _roomTimeSlotService.GetRoomDateTimeSlotsAsync(roomId, date);
            return Ok(timeSlots);
        }
    }
}
