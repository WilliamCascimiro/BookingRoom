using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using BookingRoom.Infra.Data.Context;
using BookingRoom.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Infra.Data.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<List<Booking>> GetAllByUser(Guid userID)
        {
            return await _dbSet
                    .Where(x => x.UserId == userID)
                    .Include(x => x.RoomTimeSlots)
                    .Include(x => x.User)
                    .Include(x => x.Room)
                    .ToListAsync();
        }

        public virtual async Task<List<Booking>> GetFromAllUsers()
        {
            return await _dbSet
                    .Include(x => x.RoomTimeSlots)
                    .Include(x => x.Room)
                    .Include(x => x.User)
                    .ToListAsync();
        }
    }
}
