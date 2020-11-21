using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IMonthService
    {
        Task<bool> DeleteMonth(int id);
        Task<Month> GetMonth(int id);
        PagedList<Month> GetMonths(MonthQueryFilter filters);
        Task<bool> InsertMonth(Month month);
        Task<bool> UpdateMonthAsync(Month month);
    }
}