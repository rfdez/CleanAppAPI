using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces.Repositories
{
    public interface ICleanlinessRepository
    {
        IEnumerable<Cleanliness> GetAll();

        Task<Cleanliness> GetById(int roomId, int weekId);

        IEnumerable<Cleanliness> GetByRoomId(int roomId);

        IEnumerable<Cleanliness> GetByWeekId(int weekId);

        Task Add(Cleanliness cleanliness);

        void Update(Cleanliness cleanliness);

        Task Delete(int roomId, int weekId);
    }
}
