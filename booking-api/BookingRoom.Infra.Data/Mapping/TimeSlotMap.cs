using Microsoft.EntityFrameworkCore;
using BookingRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Infra.Data.Mapping
{
    internal class TimeSlotMap : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.ToTable("sala_horario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Date).HasColumnName("data").IsRequired();
            builder.Property(x => x.Time).HasColumnName("hora").IsRequired();
            builder.Property(x => x.IsBooked).HasColumnName("reservado");

            builder.Property(x => x.RoomId).HasColumnName("id_sala").IsRequired();
            builder.HasOne(p => p.Room).WithMany(u => u.RoomTimeSlots).HasForeignKey(p => p.RoomId);

            builder.Property(x => x.BookingId).HasColumnName("id_reserva");
            builder.HasOne(p => p.Booking).WithMany(u => u.RoomTimeSlots).HasForeignKey(p => p.BookingId);

            builder.Property(a => a.RowVersion).IsRowVersion();

        }
    }
}
