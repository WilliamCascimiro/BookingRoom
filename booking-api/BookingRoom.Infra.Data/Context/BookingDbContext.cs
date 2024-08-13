using BookingRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Infra.Data.Context
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.EnsureCreated();
            //this.Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Reservas { get; set; }
        public DbSet<Room> Salas { get; set; }
        public DbSet<TimeSlot> ReservasSala { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<User>().HasData(
               new User("Administrador 1", "admin1@gmail.com", "1234", "admin"),
               new User("Administrador 2", "admin2@gmail.com", "1234", "admin"),
               new User("Usuário 1", "user1@gmail.com", "1234", "admin"));

            Guid idSala1 = Guid.NewGuid();
            Guid idSala2 = Guid.NewGuid();
            Guid idSala3 = Guid.NewGuid();

            modelBuilder.Entity<Room>().HasData(
               new Room(idSala1, "Sala principal"),
               new Room(idSala2, "Sala Diretoria"),
               new Room(idSala3, "Sala segundo piso"));

            modelBuilder.Entity<TimeSlot>().HasData(
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(09, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(10, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(11, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(12, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(13, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(14, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(15, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(16, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 12), new TimeOnly(17, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(09, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(10, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(11, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(12, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(13, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(14, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(15, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(16, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 13), new TimeOnly(17, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(09, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(10, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(11, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(12, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(13, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(14, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(15, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(16, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 14), new TimeOnly(17, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(09, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(10, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(11, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(12, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(13, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(14, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(15, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(16, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 15), new TimeOnly(17, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(09, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(10, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(11, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(12, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(13, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(14, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(15, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(16, 00)),
               new TimeSlot(idSala1, new DateOnly(2024, 08, 16), new TimeOnly(17, 00)),


               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(09, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(10, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(11, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(12, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(13, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(14, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(15, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(16, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 12), new TimeOnly(17, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(09, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(10, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(11, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(12, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(13, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(14, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(15, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(16, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 13), new TimeOnly(17, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(09, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(10, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(11, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(12, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(13, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(14, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(15, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(16, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 14), new TimeOnly(17, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(09, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(10, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(11, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(12, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(13, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(14, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(15, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(16, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 15), new TimeOnly(17, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(09, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(10, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(11, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(12, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(13, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(14, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(15, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(16, 00)),
               new TimeSlot(idSala2, new DateOnly(2024, 08, 16), new TimeOnly(17, 00)),

               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(09, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(10, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(11, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(12, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(13, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(14, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(15, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(16, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 12), new TimeOnly(17, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(09, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(10, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(11, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(12, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(13, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(14, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(15, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(16, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 13), new TimeOnly(17, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(09, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(10, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(11, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(12, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(13, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(14, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(15, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(16, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 14), new TimeOnly(17, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(09, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(10, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(11, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(12, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(13, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(14, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(15, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(16, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 15), new TimeOnly(17, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(09, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(10, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(11, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(12, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(13, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(14, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(15, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(16, 00)),
               new TimeSlot(idSala3, new DateOnly(2024, 08, 16), new TimeOnly(17, 00))
            );
        }

    }
}
