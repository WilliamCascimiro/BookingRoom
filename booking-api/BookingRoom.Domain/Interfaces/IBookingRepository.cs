using BookingRoom.Domain.Entities;

namespace BookingRoom.Domain.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetAllByUser(Guid userID);
    }
}
