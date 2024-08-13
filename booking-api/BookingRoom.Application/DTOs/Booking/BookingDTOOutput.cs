namespace BookingRoom.Application.DTOs.Booking
{
    public class BookingDTOOutput
    {
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public Guid SalaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
