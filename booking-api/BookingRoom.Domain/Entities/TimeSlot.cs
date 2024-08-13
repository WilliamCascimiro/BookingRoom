namespace BookingRoom.Domain.Entities
{
    public class TimeSlot : BaseDomain
    {
        public TimeSlot() { }

        public TimeSlot(Guid roomId, DateOnly date, TimeOnly time) 
        {
            Id = Guid.NewGuid();
            RoomId = roomId;
            Date = date;
            Time = time;
            BookingId = null;
        }

        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public Guid? BookingId { get; set; }
        public Booking Booking { get; set; }
        public bool IsBooked { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
