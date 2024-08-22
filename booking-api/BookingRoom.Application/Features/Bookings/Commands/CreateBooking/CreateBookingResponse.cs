namespace BookingRoom.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingResponse
    {
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public Guid SalaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

}
