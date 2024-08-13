namespace BookingRoom.Application.DTOs.Booking
{
    public class ListBookingResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public string Date { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
    }
}
