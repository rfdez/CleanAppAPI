using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IJobService
    {
        Task<bool> DeleteJob(int id);
        Task<Job> GetJob(int id);
        IEnumerable<Job> GetJobs();
        Task<bool> InsertJob(Job job);
        Task<bool> UpdateJobAsync(Job job);
    }
}