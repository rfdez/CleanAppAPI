using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IYearService
    {
        IEnumerable<Year> GetYears(YearQueryFilter filters);

        Task<Year> GetYear(int id);

        Task InsertYear(Year year);

        Task UpdateYearAsync(Year year);

        Task DeleteYear(int id);
    }
}