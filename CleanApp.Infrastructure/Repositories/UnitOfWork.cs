using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using System;
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
    }
}
