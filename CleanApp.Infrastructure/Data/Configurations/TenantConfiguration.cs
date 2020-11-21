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
            builder.HasKey(e => e.Id)
                    .HasName("PK_Tenant");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.TenantName)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);
        }
    }
}
