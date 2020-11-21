using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(e => e.JobDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.JobName)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.HasOne(d => d.Room)
                .WithMany(p => p.Jobs)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomJob");
        }
    }
}
