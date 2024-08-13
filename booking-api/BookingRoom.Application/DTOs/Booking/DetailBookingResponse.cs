
namespace BookingRoom.Application.DTOs.Booking
{
    public class DetailBookingResponse
    {
        public Guid roomId { get; set; }
        public DateOnly Date { get; set; }
        public List<Guid> timeSlotSelected { get; set; } = new List<Guid>();
    }
}
