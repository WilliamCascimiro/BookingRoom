using AutoMapper;
using BookingRoom.Application.DTOs.RoomDateTimeSlot;
using BookingRoom.Domain.Interfaces;

namespace BookingRoom.Application.Services
{
    public class RoomTimeSlotService : IRoomTimeSlotService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomTimeSlotRepository _roomTimeSlotRepository;
        protected readonly IMapper _mapper;

        public RoomTimeSlotService(IBookingRepository bookingRepository, IRoomTimeSlotRepository roomTimeSlotRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _roomTimeSlotRepository = roomTimeSlotRepository;
            _mapper = mapper;
        }

        public async Task<List<RoomDateTimeSlotResponse>> GetRoomDateTimeSlotsAsync(string roomId, DateTime date)
        {
            var dados = await _roomTimeSlotRepository.GetRoomDateTimeSlots(new Guid(roomId), DateOnly.FromDateTime(date));

            var responseList = new List<RoomDateTimeSlotResponse>();

            foreach (var dado in dados)
            {
                var response = new RoomDateTimeSlotResponse();
                response.id = dado.Id;
                response.Date = dado.Date.ToString();
                response.Time = dado.Time.ToString();
                response.IsBooked = dado.IsBooked;

                responseList.Add(response);
            }

            return responseList.OrderBy(x => x.Time).ToList();
        }

        public async Task<List<RoomDateTimeSlotResponse>> GetRoomDateTimeSlotsAsync(string roomId, DateTime date, string bookingId)
        {
            var dados = await _roomTimeSlotRepository.GetRoomDateTimeSlots(new Guid(roomId), DateOnly.FromDateTime(date));

            var responseList = new List<RoomDateTimeSlotResponse>();

            foreach (var dado in dados)
            {
                var response = new RoomDateTimeSlotResponse();
                response.id = dado.Id;
                response.Date = dado.Date.ToString();
                response.Time = dado.Time.ToString();
                response.IsBooked = dado.IsBooked;
                if (dado.BookingId == new Guid(bookingId))
                {
                    response.Selected = true;
                    response.IsBooked = false;
                }
                    

                responseList.Add(response);
            }

            return responseList.OrderBy(x => x.Time).ToList();
        }

        
    }
}
