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
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.WeekPageSize : filters.PageSize;

            var weeks = _unitOfWork.WeekRepository.GetAll();

            if (filters.MonthId != null)
            {
                weeks = weeks.Where(w => w.MonthId == filters.MonthId).AsEnumerable();

                if (filters.WeekValue != null)
                {
                    weeks = weeks.Where(w => w.WeekValue == filters.WeekValue).AsEnumerable();
                }
            }

            var pagedWeeks = PagedList<Week>.Create(weeks.Count() > 0 ? weeks : throw new BusinessException("No hay semanas disponibles."), filters.PageNumber, filters.PageSize);

            return pagedWeeks;
        }

        public async Task<Week> GetWeek(int id)
        {
            return await _unitOfWork.WeekRepository.GetById(id) ?? throw new BusinessException("No existe la semana solicitada.");
        }

        public async Task InsertWeek(Week week)
        {
            var month = await _unitOfWork.MonthRepository.GetById(week.MonthId) ?? throw new BusinessException("El mes asignado no existe.");
            var monthWeeks = await _unitOfWork.WeekRepository.GetWeeksByMonth(month.Id);

            if (monthWeeks.Where(w => w.WeekValue == week.WeekValue).Count() > 0)
            {
                throw new BusinessException("No puede haber un mes con semanas repetidas.");
            }

            await _unitOfWork.WeekRepository.Add(week);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateWeekAsync(Week week)
        {
            var existsWeek = await _unitOfWork.WeekRepository.GetById(week.Id) ?? throw new BusinessException("La semana que quiere editar no existe.");
            var existsMonth = await _unitOfWork.MonthRepository.GetById(week.MonthId) ?? throw new BusinessException("El mes asignado no existe.");
            var weeks = await _unitOfWork.WeekRepository.GetWeeksByMonth(existsMonth.Id);

            if (existsWeek.MonthId != week.MonthId)
            {
                if (weeks.Where(w => w.WeekValue == week.WeekValue).Count() > 0)
                {
                    throw new BusinessException("No puede haber un mes con semanas repetidas.");

                }
            }
            else
            {
                if (weeks.Except(new[] { existsWeek }).Where(w => w.WeekValue == week.WeekValue).Count() > 0)
                {
                    throw new BusinessException("Ya existe otra semana con ese valor para este mes.");
                }
            }

            _unitOfWork.WeekRepository.Update(week);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteWeek(int id)
        {
            var exists = await _unitOfWork.WeekRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe la semana que desea borrar.");
            }

            await _unitOfWork.WeekRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
