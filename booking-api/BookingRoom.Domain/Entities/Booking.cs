namespace BookingRoom.Domain.Entities
{
    public class Booking : BaseDomain
    {
        public Booking()
        {
        }

        public Booking(Guid userId, Guid roomId)
        {
            this.UserId = userId;
            this.ReservationDate = DateTime.Now;
            this.RoomId = roomId;
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<TimeSlot> RoomTimeSlots { get; set; }
        public DateTime ReservationDate { get; set; }



        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
