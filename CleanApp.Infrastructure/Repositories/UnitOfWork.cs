using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using CleanApp.Infrastructure.Repositories.Auth;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanAppDDBBContext _context;
        public UnitOfWork(CleanAppDDBBContext context)
        {
            _context = context;
        }

        public IYearRepository YearRepository => new YearRepository(_context);

        public IMonthRepository MonthRepository => new MonthRepository(_context);

        public IWeekRepository WeekRepository => new WeekRepository(_context);

        public ITenantRepository TenantRepository => new TenantRepository(_context);

        public IRoomRepository RoomRepository => new RoomRepository(_context);

        public IJobRepository JobRepository => new JobRepository(_context);

        public IAuthenticationRepository AuthenticationRepository => new AuthenticationRepository(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DetachLocal<T>(T t, int entryId) where T : BaseEntity
        {
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(t).State = EntityState.Modified;
        }
    }
}
