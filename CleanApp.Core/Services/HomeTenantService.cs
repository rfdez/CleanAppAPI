using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class HomeTenantService : IHomeTenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeTenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HomeTenant> GetHomeTenants()
        {
            return _unitOfWork.HomeTenantRepository.GetAll();
        }

        public async Task InsertHomeTenant(HomeTenant homeTenant)
        {
            await _unitOfWork.HomeTenantRepository.Add(homeTenant);
        }
    }
}
