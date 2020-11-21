using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IYearService
    {
        IEnumerable<Year> GetYears();

        Task<Year> GetYear(int id);

        Task<bool> InsertYear(Year year);

        Task<bool> UpdateYearAsync(Year year);

        Task<bool> DeleteYear(int id);
    }
}