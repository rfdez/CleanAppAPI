using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class HomeConfiguration : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.Property(e => e.HomeAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

            builder.Property(e => e.HomeCity)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.Property(e => e.HomeCountry)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.Property(e => e.HomeZipCode)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}
