
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Repositories;
using CleanApp.Infrastructure.Data;

namespace CleanApp.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(CleanAppDDBBContext context) : base(context) { }

    }
}
