using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CleanAppDDBBContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(CleanAppDDBBContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);

            _entities.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
