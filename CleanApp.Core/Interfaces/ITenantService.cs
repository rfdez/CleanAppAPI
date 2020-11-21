using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface ITenantService
    {
        IEnumerable<Tenant> GetTenants();

        Task<Tenant> GetTenant(int id);
        
        Task<bool> InsertTenant(Tenant tenant);

        Task<bool> UpdateTenantAsync(Tenant tenant);

        Task<bool> DeleteTenant(int id);
    }
}