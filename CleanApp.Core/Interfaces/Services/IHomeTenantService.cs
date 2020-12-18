using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Services
{
    public interface IHomeTenantService
    {
        IEnumerable<HomeTenant> GetHomeTenants();
        Task InsertHomeTenant(HomeTenant homeTenant);
    }
}
