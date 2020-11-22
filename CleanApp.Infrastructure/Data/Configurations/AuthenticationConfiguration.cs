using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Infrastructure.Data.Configurations
{
    public class AuthenticationConfiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.ToTable("Authentications");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CurrentUser)
                .HasColumnName("CurrentUser")
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.HasIndex(e => e.CurrentUser)
                    .HasName("UC_CurrentUser")
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
