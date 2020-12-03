using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Core.QueryFilters;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class WeekService : IWeekService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public WeekService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Week> GetWeeks(WeekQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? 4 : filters.PageSize;

            var weeks = _unitOfWork.WeekRepository.GetAll();

            if (filters.MonthId != null)
            {
                weeks = weeks.Where(w => w.MonthId == filters.MonthId).AsEnumerable();

                if (filters.WeekValue != null)
                {
                    weeks = weeks.Where(w => w.WeekValue == filters.WeekValue).AsEnumerable();
                }
            }

            var pagedWeeks = PagedList<Week>.Create(weeks, filters.PageNumber, filters.PageSize);

            return pagedWeeks;
        }

        public async Task<Week> GetWeek(int id)
        {
            return await _unitOfWork.WeekRepository.GetById(id);
        }

        public async Task<bool> InsertWeek(Week week)
        {
            await _unitOfWork.WeekRepository.Add(week);
            return true;
        }

        public async Task<bool> UpdateWeekAsync(Week week)
        {
            _unitOfWork.WeekRepository.Update(week);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWeek(int id)
        {
            await _unitOfWork.WeekRepository.Delete(id);
            return true;
        }
    }
}
