using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IWeekService
    {
        Task DeleteWeek(int id);
        Task<Week> GetWeek(int id);
        PagedList<Week> GetWeeks(WeekQueryFilter filters);
        Task InsertWeek(Week week);
        Task UpdateWeekAsync(Week week);
    }
}