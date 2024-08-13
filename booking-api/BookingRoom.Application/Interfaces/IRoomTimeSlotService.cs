using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Application.DTOs.RoomDateTimeSlot;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Entities;

namespace BookingRoom.Application.Services
{
    public interface IRoomTimeSlotService
    {
        Task<List<RoomDateTimeSlotResponse>> GetRoomDateTimeSlotsAsync(string roomId, DateTime date);
        Task<List<RoomDateTimeSlotResponse>> GetRoomDateTimeSlotsAsync(string roomId, DateTime date, string bookingId);
    }
}
