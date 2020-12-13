using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class HomeRepository : BaseRepository<Home>, IHomeRepository
    {
        public HomeRepository(CleanAppDDBBContext context) : base(context) { }
    }
}
