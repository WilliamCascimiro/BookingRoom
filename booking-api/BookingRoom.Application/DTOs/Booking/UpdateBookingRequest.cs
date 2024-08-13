namespace BookingRoom.Application.DTOs.Booking
{
    public class UpdateBookingRequest
    {
        public Guid bookingId { get; set; }
        public List<Guid> timeSlotsId { get; set; }
        public Guid roomId { get; set; }
        public Guid userId { get; set; }
    }
}
