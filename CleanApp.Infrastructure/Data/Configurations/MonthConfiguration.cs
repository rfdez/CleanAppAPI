using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class MonthConfiguration : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_Month");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(d => d.Year)
                .WithMany(p => p.Months)
                .HasForeignKey(d => d.YearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YearMonth");
        }
    }
}
