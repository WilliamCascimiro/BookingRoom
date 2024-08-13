namespace BookingRoom.Domain.Entities
{
    public class Room : BaseDomain
    {
        public Room() { }
        public Room(string name) 
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Room(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
        public ICollection<TimeSlot> RoomTimeSlots { get; set; }
    }

}
