
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class MonthRepository : BaseRepository<Month>, IMonthRepository
    {
        public MonthRepository(CleanAppDDBBContext context) : base(context){ }

        public async Task<IEnumerable<Month>> GetMonthsByYear(int yearId)
        {
            return await _entities.Where(y => y.YearId == yearId).ToListAsync();
        }
    }
}
