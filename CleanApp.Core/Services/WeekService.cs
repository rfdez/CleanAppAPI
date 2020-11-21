using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class WeekService : IWeekService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WeekService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Week> GetWeeks()
        {
            return _unitOfWork.WeekRepository.GetAll();
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
