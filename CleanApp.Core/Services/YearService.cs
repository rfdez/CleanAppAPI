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
    public class YearService : IYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public YearService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Year> GetYears(YearQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var years = _unitOfWork.YearRepository.GetAll();

            if (filters.YearValue != 0)
            {
                years = years.Where(y => y.YearValue == filters.YearValue).AsEnumerable();
            }

            var pagedYears = PagedList<Year>.Create(years.Count() > 0 ? years : throw new BusinessException("No hay años disponibles."), filters.PageNumber, filters.PageSize);

            return pagedYears;
        }

        public async Task<Year> GetYear(int id)
        {
            return await _unitOfWork.YearRepository.GetById(id) ?? throw new BusinessException("No existe el año solicitado.");
        }

        public async Task InsertYear(Year year)
        {
            var years = _unitOfWork.YearRepository.GetAll();

            foreach (var item in years)
            {
                if (item.YearValue == year.YearValue)
                {
                    throw new BusinessException("No es posible repetir un año.");
                }
            }

            await _unitOfWork.YearRepository.Add(year);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateYearAsync(Year year)
        {
            var exsists = await _unitOfWork.YearRepository.GetById(year.Id);

            if (exsists == null)
            {
                throw new BusinessException("No existe el año solicitado.");
            }

            var years = _unitOfWork.YearRepository.GetAll();

            if (years.Except(new[] { exsists }).Where(y => y.YearValue == year.YearValue).Count() > 0)
            {
                throw new BusinessException("No puedes duplicar un año.");
            }

            _unitOfWork.YearRepository.Update(year);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteYear(int id)
        {
            var exists = await _unitOfWork.YearRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe el año que desea borrar.");
            }

            await _unitOfWork.YearRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
