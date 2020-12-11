using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class YearConfiguration : IEntityTypeConfiguration<Year>
    {
        public void Configure(EntityTypeBuilder<Year> builder)
        {
            builder.HasIndex(e => e.YearValue)
                    .HasName("UC_YearValue")
                    .IsUnique();
        }
    }
}
