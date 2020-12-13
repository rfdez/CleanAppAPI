using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(CleanAppDDBBContext context) : base(context) { }


        public async Task<Tenant> GetTenantByAuthUser(string authUser)
        {
            return await _entities.FirstOrDefaultAsync(t => t.AuthUser == authUser);
        }
    }
}
