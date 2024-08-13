using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using BookingRoom.Infra.Data.Context;
using BookingRoom.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Infra.Data.Repositories
{
    public class RoomTimeSlotRepository : BaseRepository<TimeSlot>, IRoomTimeSlotRepository
    {
        readonly BookingDbContext _context;

        public RoomTimeSlotRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<List<TimeSlot>> GetAllAvailable(Guid roomId, DateOnly date)
        {
            return await _dbSet.Where(x => x.Date == date && x.RoomId == roomId && x.IsBooked == false).ToListAsync();
        }

        //public virtual async Task<List<TimeSlot>> GetAll(Guid[] timeSlotIds)
        //{
        //    return await _dbSet
        //                    .Where(x => timeSlotIds.Contains(x.Id) && x.IsBooked == true)
        //                    .ToListAsync();
        //}

        public virtual async Task<List<TimeSlot>> GetAll(Guid timeSlotId)
        {
            return await _dbSet
                            .Where(x => x.Id == timeSlotId)
                            .ToListAsync();
        }

        public virtual async Task<List<TimeSlot>> GetRoomDateTimeSlots(Guid roomId, DateOnly date)
        {
            return await _dbSet
                            .Where(x => x.RoomId == roomId && x.Date == date)
                            .ToListAsync();
        }

        public virtual async Task<List<TimeSlot>> GetTimeSlotsByBookingId(Guid bookingId)
        {
            return await _dbSet
                            .Where(x => x.BookingId == bookingId)
                            .AsNoTracking()
                            .ToListAsync();
        }
    }
}
