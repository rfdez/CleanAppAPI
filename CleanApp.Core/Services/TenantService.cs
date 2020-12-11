using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.QueryFilters;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public TenantService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task DeleteTenant(int id)
        {
            await _unitOfWork.TenantRepository.Delete(id);
        }

        public async Task<Tenant> GetTenant(int id)
        {
            return await _unitOfWork.TenantRepository.GetById(id) ?? throw new BusinessException("No existe el inquilino solicitado.");
        }

        public PagedList<Tenant> GetTenants(TenantQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var tenants = _unitOfWork.TenantRepository.GetAll();

            if (filters.TenantName != null)
            {
                tenants = tenants.Where(t => t.TenantName.Normalize().ToLower() == filters.TenantName.Normalize().ToLower()).AsEnumerable();
            }

            var pagedTenants = PagedList<Tenant>.Create(tenants.Count() > 0 ? tenants : throw new BusinessException("No hay inquilinos disponibles."), filters.PageNumber, filters.PageSize);

            return pagedTenants;
        }

        public async Task InsertTenant(Tenant tenant)
        {
            await _unitOfWork.TenantRepository.Add(tenant);
        }

        public async Task UpdateTenantAsync(Tenant tenant)
        {
            _unitOfWork.TenantRepository.Update(tenant);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
