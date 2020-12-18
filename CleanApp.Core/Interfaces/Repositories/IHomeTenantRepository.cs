using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Repositories
{
    public interface IHomeTenantRepository
    {
        IEnumerable<HomeTenant> GetAll();

        Task<HomeTenant> GetById(int homeId, int tenantId);

        Task Add(HomeTenant homeTenant);

        void Update(HomeTenant homeTenant);

        Task Delete(int homeId, int tenantId);
    }
}
