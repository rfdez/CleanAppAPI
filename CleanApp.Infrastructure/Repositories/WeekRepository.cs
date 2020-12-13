
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class WeekRepository : BaseRepository<Week>, IWeekRepository
    {
        public WeekRepository(CleanAppDDBBContext context) : base(context) { }

        public async Task<IEnumerable<Week>> GetWeeksByYear(int yearId)
        {
            return await _entities.Where(w => w.Month.YearId == yearId).ToListAsync();
        }

        public async Task<IEnumerable<Week>> GetWeeksByMonth(int monthId)
        {
            return await _entities.Where(w => w.MonthId == monthId).ToListAsync();
        }
    }
}
