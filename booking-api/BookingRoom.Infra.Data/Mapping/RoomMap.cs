using Microsoft.EntityFrameworkCore;
using BookingRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Infra.Data.Mapping
{
    internal class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("sala");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasColumnName("nome").HasMaxLength(100).IsRequired();
        }
    }
}
