using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Job> GetJobs()
        {
            return _unitOfWork.JobRepository.GetAll();
        }

        public async Task<Job> GetJob(int id)
        {
            return await _unitOfWork.JobRepository.GetById(id);
        }

        public async Task<bool> InsertJob(Job job)
        {
            await _unitOfWork.JobRepository.Add(job);
            return true;
        }

        public async Task<bool> UpdateJobAsync(Job job)
        {
            _unitOfWork.JobRepository.Update(job);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteJob(int id)
        {
            await _unitOfWork.JobRepository.Delete(id);
            return true;
        }
    }
}
