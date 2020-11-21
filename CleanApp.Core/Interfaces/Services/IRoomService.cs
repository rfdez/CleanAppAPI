using CleanApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IRoomService
    {
        Task<bool> DeleteRoom(int id);
        Task<Room> GetRoom(int id);
        IEnumerable<Room> GetRooms();
        Task<bool> InsertRoom(Room room);
        Task<bool> UpdateRoomAsync(Room room);
    }
}