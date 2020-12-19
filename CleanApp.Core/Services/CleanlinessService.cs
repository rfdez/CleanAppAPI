using CleanApp.Core.CustomEntities;
using CleanApp.Core.Entities;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.QueryFilters;
using CleanApp.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class CleanlinessService : ICleanlinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public CleanlinessService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task DeleteCleanliness(int roomId, int weekId)
        {
            var exists = await _unitOfWork.CleanlinessRepository.GetById(roomId, weekId);

            if (exists == null)
            {
                throw new BusinessException("No existe el turno de limpieza que desea borrar.");
            }

            await _unitOfWork.CleanlinessRepository.Delete(roomId, weekId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Cleanliness> GetCleanliness(int roomId, int weekId)
        {
            return await _unitOfWork.CleanlinessRepository.GetById(roomId, weekId) ?? throw new BusinessException("No existe el turno de limpieza."); ;
        }

        public PagedList<Cleanliness> GetCleanlinesses(CleanlinessQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var cleanlinesses = _unitOfWork.CleanlinessRepository.GetAll();

            if (filters.RoomId != null && filters.WeekId != null)
            {
                cleanlinesses = cleanlinesses.Where(c => c.RoomId == filters.RoomId && c.WeekId == filters.WeekId).AsEnumerable();
            }
            else if (filters.RoomId != null)
            {
                cleanlinesses = _unitOfWork.CleanlinessRepository.GetByRoomId((int)filters.RoomId);
            }
            else if (filters.WeekId != null)
            {
                cleanlinesses = _unitOfWork.CleanlinessRepository.GetByWeekId((int)filters.WeekId);
            }

            if (filters.TenantId != null)
            {
                cleanlinesses = cleanlinesses.Where(c => c.TenantId == filters.TenantId).AsEnumerable();
            }

            if (filters.CleanlinessDone != null)
            {
                cleanlinesses = cleanlinesses.Where(c => c.CleanlinessDone == filters.CleanlinessDone).AsEnumerable();
            }

            var pagedHomeTenants = PagedList<Cleanliness>.Create(cleanlinesses.Count() > 0 ? cleanlinesses : throw new BusinessException("No hay turnos de limpieza."), filters.PageNumber, filters.PageSize);

            return pagedHomeTenants;
        }

        public async Task InsertCleanliness(Cleanliness cleanliness)
        {
            var exists = await _unitOfWork.CleanlinessRepository.GetById(cleanliness.RoomId, cleanliness.WeekId);

            if (exists != null)
            {
                throw new BusinessException("Ya existe un turno de limpieza para esa habitación en esa semana.");
            }

            var existRoom = await _unitOfWork.RoomRepository.GetById(cleanliness.RoomId);

            if (existRoom == null)
            {
                throw new BusinessException("No existe la habitación.");
            }

            var existWeek = await _unitOfWork.WeekRepository.GetById(cleanliness.WeekId);

            if (existWeek == null)
            {
                throw new BusinessException("No existe la semana.");
            }

            var existTenant = await _unitOfWork.TenantRepository.GetById((int)cleanliness.TenantId);

            if (existTenant == null)
            {
                throw new BusinessException("No existe el inquilino.");
            }

            cleanliness.CleanlinessDone = false;
            await _unitOfWork.CleanlinessRepository.Add(cleanliness);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCleanlinessAsync(Cleanliness cleanliness)
        {
            var exists = await _unitOfWork.CleanlinessRepository.GetById(cleanliness.RoomId, cleanliness.WeekId);

            if (exists == null)
            {
                throw new BusinessException("No existe el turno de limpieza que desea editar.");
            }

            var existTenant = await _unitOfWork.TenantRepository.GetById((int)cleanliness.TenantId);

            if (existTenant == null)
            {
                throw new BusinessException("No existe el inquilino.");
            }

            _unitOfWork.CleanlinessRepository.Update(cleanliness);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
