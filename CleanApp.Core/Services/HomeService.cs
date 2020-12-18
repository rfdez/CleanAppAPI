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
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public HomeService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public PagedList<Home> GetHomes(HomeQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var homes = _unitOfWork.HomeRepository.GetAll();

            var pagedHomes = PagedList<Home>.Create(homes.Count() > 0 ? homes : throw new BusinessException("No hay viviendas disponibles."), filters.PageNumber, filters.PageSize);

            return pagedHomes;
        }
        public async Task<Home> GetHome(int id)
        {
            return await _unitOfWork.HomeRepository.GetById(id) ?? throw new BusinessException("No existe la vivienda solicitada.");
        }
        public async Task InsertHome(Home home)
        {
            await _unitOfWork.HomeRepository.Add(home);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateHomeAsync(Home home)
        {
            var exists = await _unitOfWork.HomeRepository.GetById(home.Id);

            if (exists == null)
            {
                throw new BusinessException("No existe la vivienda a actualizar.");
            }

            _unitOfWork.HomeRepository.Update(home);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteHome(int id)
        {
            var exists = await _unitOfWork.HomeRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe la vivienda a borrar.");
            }

            await _unitOfWork.HomeRepository.Delete(id);
        }
    }
}
