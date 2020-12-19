using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Services
{
    public interface ICleanlinessService
    {
        PagedList<Cleanliness> GetCleanlinesses(CleanlinessQueryFilter filters);
        Task<Cleanliness> GetCleanliness(int roomId, int weekId);
        Task InsertCleanliness(Cleanliness cleanliness);
        Task UpdateCleanlinessAsync(Cleanliness cleanliness);
        Task DeleteCleanliness(int roomId, int weekId);
    }
}
