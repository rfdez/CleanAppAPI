using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IMonthService
    {
        Task<bool> DeleteMonth(int id);
        Task<Month> GetMonth(int id);
        IEnumerable<Month> GetMonths();
        Task<bool> InsertMonth(Month month);
        Task<bool> UpdateMonthAsync(Month month);
    }
}