using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.QueryFilters;
using System.Linq;
using System.Threading.Tasks;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;

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
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var months = _unitOfWork.MonthRepository.GetAll();

            if (filters.YearId != null)
            {
                months = months.Where(m => m.YearId == filters.YearId).AsEnumerable();

                if (filters.MonthValue != null)
                {
                    months = months.Where(m => m.MonthValue == filters.MonthValue).AsEnumerable();
                }
            }

            var pagedMonths = PagedList<Month>.Create(months, filters.PageNumber, filters.PageSize);

            return pagedMonths;
        }

        public async Task<Month> GetMonth(int id)
        {
            return await _unitOfWork.MonthRepository.GetById(id);
        }

        public async Task<bool> InsertMonth(Month month)
        {
            var months = await _unitOfWork.MonthRepository.GetMonthsByYear(month.YearId);

            var exist = months.Where(m => m.MonthValue == month.MonthValue).Count();

            if (exist > 0)
            {
                throw new BusinessException("No se puede repetir el mes.");
            }

            await _unitOfWork.MonthRepository.Add(month);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMonthAsync(Month month)
        {
            _unitOfWork.MonthRepository.Update(month);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMonth(int id)
        {
            await _unitOfWork.MonthRepository.Delete(id);
            return true;
        }
    }
}
