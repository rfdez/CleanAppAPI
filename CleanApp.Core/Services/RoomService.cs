using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Room> GetRooms()
        {
            return _unitOfWork.RoomRepository.GetAll();
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _unitOfWork.RoomRepository.GetById(id);
        }

        public async Task<bool> InsertRoom(Room room)
        {
            await _unitOfWork.RoomRepository.Add(room);
            return true;
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            _unitOfWork.RoomRepository.Update(room);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            await _unitOfWork.RoomRepository.Delete(id);
            return true;
        }
    }
}
