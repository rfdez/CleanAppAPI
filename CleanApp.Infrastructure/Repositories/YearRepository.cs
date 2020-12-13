
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class YearRepository : BaseRepository<Year>, IYearRepository
    {
        public YearRepository(CleanAppDDBBContext context) : base(context) { }

    }
}
