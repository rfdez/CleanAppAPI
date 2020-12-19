using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class HomeTenantRepository : IHomeTenantRepository
    {
        private readonly CleanAppDDBBContext _context;
        protected readonly DbSet<HomeTenant> _entities;

        public HomeTenantRepository(CleanAppDDBBContext context)
        {
            _context = context;
            _entities = _context.Set<HomeTenant>();
        }

        public IEnumerable<HomeTenant> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<HomeTenant> GetById(int homeId, int tenantId)
        {
            return await _entities.FirstOrDefaultAsync(e => e.HomeId == homeId && e.TenantId == tenantId);
        }

        public async Task Add(HomeTenant homeTenant)
        {
            await _entities.AddAsync(homeTenant);
        }

        public void Update(HomeTenant homeTenant)
        {
            _context.Entry(_entities.Local.FirstOrDefault(e => e.HomeId == homeTenant.HomeId && e.TenantId == homeTenant.TenantId)).State = EntityState.Deleted;
            _context.Entry(homeTenant).State = EntityState.Modified;

            _context.Update(homeTenant);
        }

        public async Task Delete(int homeId, int tenantId)
        {
            HomeTenant homeTenant = await GetById(homeId, tenantId);

            _entities.Remove(homeTenant);
        }

        public IEnumerable<HomeTenant> GetByHomeId(int homeId)
        {
            return _entities.Where(ht => ht.HomeId == homeId).AsEnumerable();
        }

        public IEnumerable<HomeTenant> GetByTenantId(int tenantId)
        {
            return _entities.Where(ht => ht.TenantId == tenantId).AsEnumerable();
        }
    }
}
