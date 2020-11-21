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
        private readonly IYearRepository _yearRepository;
        private readonly IMonthRepository _monthRepository;
        private readonly IWeekRepository _weekRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IJobRepository _jobRepository;

        public UnitOfWork(CleanAppDDBBContext context)
        {
            _context = context;
        }

        public IYearRepository YearRepository => _yearRepository ?? new YearRepository(_context);

        public IMonthRepository MonthRepository => _monthRepository ?? new MonthRepository(_context);

        public IWeekRepository WeekRepository => _weekRepository ?? new WeekRepository(_context);

        public ITenantRepository TenantRepository => _tenantRepository ?? new TenantRepository(_context);

        public IRoomRepository RoomRepository => _roomRepository ?? new RoomRepository(_context);

        public IJobRepository JobRepository => _jobRepository ?? new JobRepository(_context);

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
