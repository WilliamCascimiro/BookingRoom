using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using BookingRoom.Infra.Data.Context;
using BookingRoom.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Infra.Data.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        protected readonly BookingDbContext _context;

        public RoomRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
