namespace BookingRoom.Application.DTOs.RoomDateTimeSlot
{
    public class RoomDateTimeSlotResponse
    {
        public Guid id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool IsBooked { get; set; }
        public bool Selected { get; set; }
    }
}
