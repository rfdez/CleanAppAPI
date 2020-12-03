using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces
{
    public interface IWeekRepository : IRepository<Week>
    {
        Task<IEnumerable<Week>> GetWeeksByYear(int yearId);

        Task<IEnumerable<Week>> GetWeeksByMonth(int monthId);
    }
}
