using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;

namespace BookingRoom.Application.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAll();
    }
}
