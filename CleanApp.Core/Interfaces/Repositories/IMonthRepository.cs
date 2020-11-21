using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces
{
    public interface IMonthRepository : IRepository<Month>
    {
        Task<IEnumerable<Month>> GetMonthsByYear(int yearId);
    }
}
