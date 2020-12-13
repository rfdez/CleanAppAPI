using CleanApp.Core.Entities;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Repositories
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        Task<Tenant> GetTenantByAuthUser(string authUser);
    }
}
