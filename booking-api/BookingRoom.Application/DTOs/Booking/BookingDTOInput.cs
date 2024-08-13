using BookingRoom.Domain.Entities;

namespace BookingRoom.Application.DTOs.Booking
{
    public class BookingDTOInput
    {
        public Guid roomId { get; set; }
        public Guid userId { get; set; }
        public DateOnly bookingDate { get; set; }
        public TimeOnly bookingTime { get; set; }
    }
}
