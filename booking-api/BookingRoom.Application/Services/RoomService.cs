using BookingRoom.Application.DTOs.Booking;
using BookingRoom.Application.Features.Common;
using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using System.Net;

namespace BookingRoom.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAll()
        {
            var rooms = await _roomRepository.GetAll();

            return rooms;
        }
    }
}
