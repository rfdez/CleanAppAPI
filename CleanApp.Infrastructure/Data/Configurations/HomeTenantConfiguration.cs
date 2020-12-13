using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class HomeTenantConfiguration : IEntityTypeConfiguration<HomeTenant>
    {
        public void Configure(EntityTypeBuilder<HomeTenant> builder)
        {
            builder.HasKey(e => new { e.HomeId, e.TenantId });

            builder.HasOne(d => d.Home)
                .WithMany(p => p.Tenants)
                .HasForeignKey(d => d.HomeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HomeTenant");

            builder.HasOne(d => d.Tenant)
                .WithMany(p => p.Homes)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantHome");
        }
    }
}
