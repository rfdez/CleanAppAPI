using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IYearService
    {
        PagedList<Year> GetYears(YearQueryFilter filters);

        Task<Year> GetYear(int id);

        Task InsertYear(Year year);

        Task UpdateYearAsync(Year year);

        Task DeleteYear(int id);
    }
}