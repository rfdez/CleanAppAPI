using System;
using CleanApp.Core.Entities;
using CleanApp.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CleanApp.Infrastructure.Data
{
    public partial class CleanAppDDBBContext : DbContext
    {
        public CleanAppDDBBContext()
        {
        }

        public CleanAppDDBBContext(DbContextOptions<CleanAppDDBBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cleanliness> Cleanliness { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Week> Weeks { get; set; }
        public virtual DbSet<Year> Years { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CleanlinessConfiguration());

            modelBuilder.ApplyConfiguration(new JobConfiguration());

            modelBuilder.ApplyConfiguration(new MonthConfiguration());

            modelBuilder.ApplyConfiguration(new RoomConfiguration());

            modelBuilder.ApplyConfiguration(new TenantConfiguration());

            modelBuilder.ApplyConfiguration(new WeekConfiguration());

            modelBuilder.ApplyConfiguration(new YearConfiguration());
        }
    }
}
