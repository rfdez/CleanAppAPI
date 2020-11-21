using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class YearConfiguration : IEntityTypeConfiguration<Year>
    {
        public void Configure(EntityTypeBuilder<Year> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_Year");

            builder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}
