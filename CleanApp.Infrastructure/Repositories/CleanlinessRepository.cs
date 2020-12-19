using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class CleanlinessRepository : ICleanlinessRepository
    {
        private readonly CleanAppDDBBContext _context;
        protected readonly DbSet<Cleanliness> _entities;

        public CleanlinessRepository(CleanAppDDBBContext context)
        {
            _context = context;
            _entities = _context.Set<Cleanliness>();
        }

        public async Task Add(Cleanliness cleanliness)
        {
            await _entities.AddAsync(cleanliness);
        }

        public async Task Delete(int roomId, int weekId)
        {
            Cleanliness cleanliness = await GetById(roomId, weekId);

            _entities.Remove(cleanliness);
        }

        public IEnumerable<Cleanliness> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<Cleanliness> GetById(int roomId, int weekId)
        {
            return await _entities.FirstOrDefaultAsync(c => c.RoomId == roomId && c.WeekId == weekId);
        }

        public void Update(Cleanliness cleanliness)
        {
            _context.Entry(_entities.Local.FirstOrDefault(e => e.RoomId == cleanliness.RoomId && e.WeekId == cleanliness.WeekId)).State = EntityState.Deleted;
            _context.Entry(cleanliness).State = EntityState.Modified;

            _context.Update(cleanliness);
        }

        public IEnumerable<Cleanliness> GetByRoomId(int roomId)
        {
            return _entities.Where(c => c.RoomId == roomId).AsEnumerable();
        }

        public IEnumerable<Cleanliness> GetByWeekId(int weekId)
        {
            return _entities.Where(c => c.WeekId == weekId).AsEnumerable();
        }
    }
}
