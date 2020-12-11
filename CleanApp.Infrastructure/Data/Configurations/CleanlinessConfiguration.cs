using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class CleanlinessConfiguration : IEntityTypeConfiguration<Cleanliness>
    {
        public void Configure(EntityTypeBuilder<Cleanliness> builder)
        {
            builder.HasKey(e => new { e.RoomId, e.WeekId })
                    .HasName("PK_RoomWeek");

            builder.HasOne(d => d.Room)
                .WithMany(p => p.Cleanlinesses)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomCleanliness");

            builder.HasOne(d => d.Tenant)
                .WithMany(p => p.Cleanlinesses)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_TenantCleanliness");

            builder.HasOne(d => d.Week)
                .WithMany(p => p.Cleanlinesses)
                .HasForeignKey(d => d.WeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WeekCleanliness");
        }
    }
}
