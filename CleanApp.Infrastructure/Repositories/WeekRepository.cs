
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class WeekRepository : BaseRepository<Week>, IWeekRepository
    {
        public WeekRepository(CleanAppDDBBContext context) : base(context) { }

    }
}
