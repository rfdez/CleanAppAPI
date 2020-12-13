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
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public JobService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Job> GetJobs(JobQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var jobs = _unitOfWork.JobRepository.GetAll();

            var pagedJobs = PagedList<Job>.Create(jobs.Count() > 0 ? jobs : throw new BusinessException("No hay tareas disponibles."), filters.PageNumber, filters.PageSize);

            return pagedJobs;
        }

        public async Task<Job> GetJob(int id)
        {
            return await _unitOfWork.JobRepository.GetById(id) ?? throw new BusinessException("No existe la tarea solicitada.");
        }

        public async Task InsertJob(Job job)
        {
            await _unitOfWork.JobRepository.Add(job);
        }

        public async Task UpdateJobAsync(Job job)
        {
            _unitOfWork.JobRepository.Update(job);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteJob(int id)
        {
            var exists = await _unitOfWork.JobRepository.GetById(id);

            if (exists == null)
            {
                throw new BusinessException("No existe la tarea que desea borrar.");
            }

            await _unitOfWork.JobRepository.Delete(id);
        }
    }
}
