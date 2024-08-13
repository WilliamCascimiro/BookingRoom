using Microsoft.EntityFrameworkCore;
using BookingRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Infra.Data.Mapping
{
    internal class BookingMap : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("reserva");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.ReservationDate).HasColumnName("data_criacao_reserva");

            builder.Property(x => x.UserId).HasColumnName("id_usuario").IsRequired();
            builder.HasOne(p => p.User).WithMany(u => u.Reservas).HasForeignKey(p => p.UserId);

            builder.Property(x => x.RoomId).HasColumnName("id_sala").IsRequired();
            builder.HasOne(p => p.Room).WithMany().HasForeignKey(p => p.RoomId);
        }
    }
}
