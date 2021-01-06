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

            if (filters.RoomId != 0)
            {
                jobs = jobs.Where(j => j.RoomId == filters.RoomId).AsEnumerable();
            }

            if (filters.JobName != null)
            {
                jobs = jobs.Where(j => j.JobName.ToLower().Contains(filters.JobName)).AsEnumerable();
            }

            if (filters.JobDescription != null)
            {
                jobs = jobs.Where(j => j.JobDescription.ToLower().Contains(filters.JobDescription)).AsEnumerable();
            }

            var pagedJobs = PagedList<Job>.Create(jobs.Count() > 0 ? jobs : throw new BusinessException("No hay tareas disponibles."), filters.PageNumber, filters.PageSize);

            return pagedJobs;
        }

        public async Task<Job> GetJob(int id)
        {
            return await _unitOfWork.JobRepository.GetById(id) ?? throw new BusinessException("No existe la tarea solicitada.");
        }

        public async Task InsertJob(Job job)
        {
            var existsRoom = await _unitOfWork.RoomRepository.GetById(job.RoomId);

            if (existsRoom == null)
            {
                throw new BusinessException("No existe la habitación a asignar.");
            }

            await _unitOfWork.JobRepository.Add(job);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateJobAsync(Job job)
        {
            var exists = await _unitOfWork.JobRepository.GetById(job.Id);

            if (exists == null)
            {
                throw new BusinessException("No existe la tarea solicitada.");
            }

            if (job.RoomId != exists.RoomId)
            {
                var existsRoom = await _unitOfWork.RoomRepository.GetById(job.RoomId);

                if (existsRoom == null)
                {
                    throw new BusinessException("No existe la habitación a asignar.");
                }
            }

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
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
