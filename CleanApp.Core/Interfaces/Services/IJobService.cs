using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IJobService
    {
        PagedList<Job> GetJobs(JobQueryFilter filters);
        Task<Job> GetJob(int id);
        Task InsertJob(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJob(int id);
    }
}