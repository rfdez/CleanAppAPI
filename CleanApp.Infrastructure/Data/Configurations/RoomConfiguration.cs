using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

            builder.HasOne(d => d.Home)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeRoom");
        }
    }
}
