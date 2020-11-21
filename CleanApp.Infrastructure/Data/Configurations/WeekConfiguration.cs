using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class WeekConfiguration : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_Week");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(d => d.Month)
                .WithMany(p => p.Weeks)
                .HasForeignKey(d => d.MonthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MonthWeek");
        }
    }
}
