using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class YearService : IYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        public YearService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Year> GetYears(YearQueryFilter filters)
        {
            if (filters.YearValue != null)
            {
                var years = _unitOfWork.YearRepository.GetAll();
                return years.Where(y => y.YearValue == filters.YearValue).Count() > 0 ? years.Where(y => y.YearValue == filters.YearValue) : throw new BusinessException("No existe el año con el valor indicado.");
            }

            return _unitOfWork.YearRepository.GetAll().Count() > 0 ? _unitOfWork.YearRepository.GetAll() : throw new BusinessException("No existen años en nuestros datos.");
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
