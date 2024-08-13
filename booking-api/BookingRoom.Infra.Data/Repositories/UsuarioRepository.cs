using BookingRoom.Domain.Entities;
using BookingRoom.Domain.Interfaces;
using BookingRoom.Infra.Data.Context;
using BookingRoom.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly BookingDbContext _context;

        public UsuarioRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string email, string password)
        {
            var user = await _context.Users
                        .Where(u =>
                                    u.Email.ToUpper() == email.ToUpper() &&
                                    u.Password.ToUpper() == password.ToUpper()
                              )
                        .FirstOrDefaultAsync();

            return user;
        }
    }
}
