using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IRoomService
    {
        Task DeleteRoom(int id);
        Task<Room> GetRoom(int id);
        PagedList<Room> GetRooms(RoomQueryFilter filters);
        Task InsertRoom(Room room);
        Task UpdateRoomAsync(Room room);
    }
}