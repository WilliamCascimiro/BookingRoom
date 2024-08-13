using Microsoft.EntityFrameworkCore;
using BookingRoom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Infra.Data.Mapping
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Name).HasColumnName("nome").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasColumnName("senha").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Role).HasColumnName("tipo_usuario").HasMaxLength(100).IsRequired();
        }
    }
}
