using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class MonthService : IMonthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MonthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Month> GetMonths()
        {
            return _unitOfWork.MonthRepository.GetAll();
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
