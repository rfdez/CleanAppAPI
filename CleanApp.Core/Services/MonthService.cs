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
    public class MonthService : IMonthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public MonthService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Month> GetMonths(MonthQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.MonthPageSize : filters.PageSize;

            var months = _unitOfWork.MonthRepository.GetAll();

            if (filters.YearId != null)
            {
                months = months.Where(m => m.YearId == filters.YearId).AsEnumerable();

                if (filters.MonthValue != null)
                {
                    months = months.Where(m => m.MonthValue == filters.MonthValue).AsEnumerable();
                }
            }

            var pagedMonths = PagedList<Month>.Create(months.Count() > 0 ? months : throw new BusinessException("No hay meses disponibles."), filters.PageNumber, filters.PageSize);

            return pagedMonths;
        }

        public async Task<Month> GetMonth(int id)
        {
            return await _unitOfWork.MonthRepository.GetById(id) ?? throw new BusinessException("No existe el mes solicitado.");
        }

        public async Task InsertMonth(Month month)
        {
            var year = await _unitOfWork.YearRepository.GetById(month.YearId) ?? throw new BusinessException("El año asignado no exixte.");
            var yearMonths = await _unitOfWork.MonthRepository.GetMonthsByYear(year.Id);

            if (yearMonths.Where(m => m.MonthValue == month.MonthValue).Count() > 0)
            {
                throw new BusinessException("No puede haber un año con meses repetidos.");
            }

            await _unitOfWork.MonthRepository.Add(month);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMonthAsync(Month month)
        {
            var existMonth = await _unitOfWork.MonthRepository.GetById(month.Id) ?? throw new BusinessException("El mes que quiere editar no existe.");
            var existYear = await _unitOfWork.YearRepository.GetById(month.YearId) ?? throw new BusinessException("El año asignado no exixte.");
            var months = await _unitOfWork.MonthRepository.GetMonthsByYear(existYear.Id);

            if (existMonth.YearId != month.YearId)
            {
                if (months.Where(m => m.MonthValue == month.MonthValue).Count() > 0)
                {
                    throw new BusinessException("No puede haber un año con meses repetidos.");
                }
            }
            else
            {
                if (months.Except(new[] { existMonth }).Where(m => m.MonthValue == month.MonthValue).Count() > 0)
                {
                    throw new BusinessException("Ya existe otro mes con ese valor para este año.");
                }
            }

            _unitOfWork.MonthRepository.Update(month);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMonth(int id)
        {
            var exists = await _unitOfWork.MonthRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe el mes que desea borrar.");
            }

            await _unitOfWork.MonthRepository.Delete(id);
        }
    }
}
