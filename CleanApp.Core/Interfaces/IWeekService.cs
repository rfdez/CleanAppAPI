using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IWeekService
    {
        Task<bool> DeleteWeek(int id);
        Task<Week> GetWeek(int id);
        IEnumerable<Week> GetWeeks();
        Task<bool> InsertWeek(Week week);
        Task<bool> UpdateWeekAsync(Week week);
    }
}