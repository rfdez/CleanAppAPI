using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Services
{
    public interface IHomeService
    {
        PagedList<Home> GetHomes(HomeQueryFilter filters);
        Task<Home> GetHome(int id);
        Task InsertHome(Home home);
        Task UpdateHomeAsync(Home home);
        Task DeleteHome(int id);
    }
}
