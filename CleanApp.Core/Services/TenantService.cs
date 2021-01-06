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
        public PagedList<Tenant> GetTenants(TenantQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var tenants = _unitOfWork.TenantRepository.GetAll();

            if (filters.TenantName != null)
            {
                tenants = tenants.Where(t => t.TenantName.ToLower().Contains(filters.TenantName)).AsEnumerable();
            }

            if (filters.AuthUser != null)
            {
                tenants = tenants.Where(t => t.AuthUser.ToLower().Contains(filters.AuthUser)).AsEnumerable();
            }

            var pagedTenants = PagedList<Tenant>.Create(tenants.Count() > 0 ? tenants : throw new BusinessException("No hay inquilinos disponibles."), filters.PageNumber, filters.PageSize);

            return pagedTenants;
        }

        public async Task<Tenant> GetTenant(int id)
        {
            return await _unitOfWork.TenantRepository.GetById(id) ?? throw new BusinessException("No existe el inquilino solicitado.");
        }

        public async Task InsertTenant(Tenant tenant)
        {
            var exists = await _unitOfWork.TenantRepository.GetTenantByAuthUser(tenant.AuthUser);

            if (exists != null)
            {
                throw new BusinessException("Ya existe un inquilino asociado a las credenciales proporcionadas.");
            }

            var authUser = await _unitOfWork.AuthenticationRepository.GetAuthenticationByUserLogin(tenant.AuthUser);

            if (authUser == null)
            {
                throw new BusinessException("El inquilino debe tener unas credenciales de inicio de sesión.");
            }

            await _unitOfWork.TenantRepository.Add(tenant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTenantAsync(Tenant tenant)
        {
            var exists = await _unitOfWork.TenantRepository.GetById(tenant.Id);

            if (exists == null)
            {
                throw new BusinessException("No existe el inquilino a actualizar.");
            }

            if (tenant.AuthUser != exists.AuthUser)
            {
                var tenants = _unitOfWork.TenantRepository.GetAll();

                if (tenants.Except(new[] { exists }).Where(t => t.AuthUser == tenant.AuthUser).Count() > 0)
                {
                    throw new BusinessException("Las nuevas credenciales del actual inquilino ya están asociadas a otro inquilino.");
                }

                var authUser = await _unitOfWork.AuthenticationRepository.GetAuthenticationByUserLogin(tenant.AuthUser);

                if (authUser == null)
                {
                    throw new BusinessException("Las nuevas credenciales del inquilino deben existir.");
                }
            }

            _unitOfWork.TenantRepository.Update(tenant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTenant(int id)
        {
            var exists = await _unitOfWork.TenantRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe el inquilino a borrar.");
            }

            await _unitOfWork.TenantRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
