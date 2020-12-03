using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IWeekService
    {
        Task<bool> DeleteWeek(int id);
        Task<Week> GetWeek(int id);
        PagedList<Week> GetWeeks(WeekQueryFilter filters);
        Task<bool> InsertWeek(Week week);
        Task<bool> UpdateWeekAsync(Week week);
    }
}