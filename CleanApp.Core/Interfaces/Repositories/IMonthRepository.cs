using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Repositories
{
    public interface IMonthRepository : IRepository<Month>
    {
        Task<IEnumerable<Month>> GetMonthsByYear(int yearId);
    }
}
