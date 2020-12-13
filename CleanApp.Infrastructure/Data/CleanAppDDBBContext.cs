using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        public virtual DbSet<Authentication> Authentications { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<HomeTenant> HomeTenant { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
