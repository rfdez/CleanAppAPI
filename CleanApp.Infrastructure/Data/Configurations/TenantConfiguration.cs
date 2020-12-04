using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

            builder.Property(e => e.AuthUser)
                .HasColumnName("AuthUser")
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.HasIndex(e => e.AuthUser)
                    .HasName("UC_AuthUser")
                    .IsUnique();
        }
    }
}
