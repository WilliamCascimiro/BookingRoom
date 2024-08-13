using BookingRoom.Domain.Entities;

namespace BookingRoom.Domain.Interfaces
{
    public interface IRoomTimeSlotRepository : IBaseRepository<TimeSlot>
    {
        Task<List<TimeSlot>> GetAllAvailable(Guid roomId, DateOnly date);
        Task<List<TimeSlot>> GetAll(Guid timeSlotIds);
        Task<List<TimeSlot>> GetRoomDateTimeSlots(Guid roomId, DateOnly date);
        Task<List<TimeSlot>> GetTimeSlotsByBookingId(Guid bookingId);
    }
}
