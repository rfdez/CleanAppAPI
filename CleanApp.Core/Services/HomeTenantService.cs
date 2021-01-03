using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.QueryFilters;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class HomeTenantService : IHomeTenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public HomeTenantService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<HomeTenant> GetHomeTenants(HomeTenantQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var homeTenants = _unitOfWork.HomeTenantRepository.GetAll();

            if (filters.HomeId != 0 && filters.TenantId != 0)
            {
                homeTenants = homeTenants.Where(ht => ht.HomeId == filters.HomeId && ht.TenantId == filters.TenantId).AsEnumerable();
            }
            else if (filters.HomeId != 0)
            {
                homeTenants = _unitOfWork.HomeTenantRepository.GetByHomeId(filters.HomeId);
            }
            else if (filters.TenantId != 0)
            {
                homeTenants = _unitOfWork.HomeTenantRepository.GetByTenantId(filters.TenantId);
            }

            if (filters.TenantRole != null)
            {
                homeTenants = homeTenants.Where(ht => ht.TenantRole == filters.TenantRole).AsEnumerable();
            }

            var pagedHomeTenants = PagedList<HomeTenant>.Create(homeTenants.Count() > 0 ? homeTenants : throw new BusinessException("No hay relaciones disponibles."), filters.PageNumber, filters.PageSize);

            return pagedHomeTenants;
        }

        public async Task<HomeTenant> GetHomeTenant(int homeId, int tenantId)
        {
            return await _unitOfWork.HomeTenantRepository.GetById(homeId, tenantId) ?? throw new BusinessException("El usuario indicado to tiene ninguna relacion con la vivienda indicada.");
        }

        public async Task InsertHomeTenant(HomeTenant homeTenant)
        {
            var exists = await _unitOfWork.HomeTenantRepository.GetById(homeTenant.HomeId, homeTenant.TenantId);

            if (exists != null)
            {
                throw new BusinessException("No es posible que un inquilino tenga más de una relación con una vivienda.");
            }

            await _unitOfWork.HomeTenantRepository.Add(homeTenant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateHomeTenantAsync(HomeTenant homeTenant)
        {
            var exists = await _unitOfWork.HomeTenantRepository.GetById(homeTenant.HomeId, homeTenant.TenantId);

            if (exists == null)
            {
                throw new BusinessException("No existe la relación");
            }

            _unitOfWork.HomeTenantRepository.Update(homeTenant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteHomeTenant(int homeId, int tenantId)
        {
            var exists = await _unitOfWork.HomeTenantRepository.GetById(homeId, tenantId);

            if (exists == null)
            {
                throw new BusinessException("No existe la relación");
            }

            await _unitOfWork.HomeTenantRepository.Delete(homeId, tenantId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
