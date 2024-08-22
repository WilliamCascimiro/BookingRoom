namespace BookingRoom.Application.DTOs.Booking
{
    public class CreateBookingRequestOld
    {
        public List<Guid> id { get; set; }
        public Guid roomId { get; set; }
        public Guid userId { get; set; } 
    }
}
