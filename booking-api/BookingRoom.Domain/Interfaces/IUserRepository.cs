using BookingRoom.Domain.Entities;

namespace BookingRoom.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUser(string email, string password);
    }
}
