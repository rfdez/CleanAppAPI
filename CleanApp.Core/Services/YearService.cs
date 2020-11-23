using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using System.Collections.Generic;
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

        public IEnumerable<Year> GetYears()
        {
            return _unitOfWork.YearRepository.GetAll() ?? throw new BusinessException("No existen años en nuestros datos.");
        }

        public async Task<Year> GetYear(int id)
        {
            return await _unitOfWork.YearRepository.GetById(id) ?? throw new BusinessException("No existe el año solicitado.");
        }

        public async Task<bool> InsertYear(Year year)
        {
            var years = _unitOfWork.YearRepository.GetAll();

            foreach (var item in years)
            {
                if (item.YearValue == year.YearValue)
                {
                    return false;
                }
            }

            await _unitOfWork.YearRepository.Add(year);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateYearAsync(Year year)
        {
            var exsist = await _unitOfWork.YearRepository.GetById(year.Id);

            if (exsist == null)
            {
                throw new BusinessException("No existe el año solicitado.");
            }

            exsist.YearValue = year.YearValue;
            _unitOfWork.YearRepository.Update(year);

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> DeleteYear(int id)
        {
            await _unitOfWork.YearRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
