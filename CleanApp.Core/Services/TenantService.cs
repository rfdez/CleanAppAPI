using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteTenant(int id)
        {
            await _unitOfWork.TenantRepository.Delete(id);
            return true;
        }

        public async Task<Tenant> GetTenant(int id)
        {
            return await _unitOfWork.TenantRepository.GetById(id);
        }

        public IEnumerable<Tenant> GetTenants()
        {
            return _unitOfWork.TenantRepository.GetAll();
        }

        public async Task<bool> InsertTenant(Tenant tenant)
        {
            await _unitOfWork.TenantRepository.Add(tenant);
            return true;
        }

        public async Task<bool> UpdateTenantAsync(Tenant tenant)
        {
            _unitOfWork.TenantRepository.Update(tenant);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
