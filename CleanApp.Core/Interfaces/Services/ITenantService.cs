using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface ITenantService
    {
        PagedList<Tenant> GetTenants(TenantQueryFilter filters);

        Task<Tenant> GetTenant(int id);

        Task InsertTenant(Tenant tenant);

        Task UpdateTenantAsync(Tenant tenant);

        Task DeleteTenant(int id);
    }
}