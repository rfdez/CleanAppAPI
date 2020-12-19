using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Services
{
    public interface IHomeTenantService
    {
        PagedList<HomeTenant> GetHomeTenants(HomeTenantQueryFilter filters);
        Task<HomeTenant> GetHomeTenant(int homeId, int tenantId);
        Task InsertHomeTenant(HomeTenant homeTenant);
        Task UpdateHomeTenantAsync(HomeTenant homeTenant);
        Task DeleteHomeTenant(int homeId, int tenantId);
    }
}
