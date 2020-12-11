using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.ToTable("Authentications");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserLogin)
                .HasColumnName("UserLogin")
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.HasIndex(e => e.UserLogin)
                    .HasName("UC_UserLogin")
                    .IsUnique();

            builder.Property(e => e.UserName)
                .HasColumnName("UserName")
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.Property(e => e.UserPassword)
                .HasColumnName("UserPassword")
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.UserRole)
                .HasColumnName("UserRole")
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasConversion(
                    c => c.ToString(),
                    c => (RoleType)Enum.Parse(typeof(RoleType), c)
                );
        }
    }
}
