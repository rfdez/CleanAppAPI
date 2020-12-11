using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Enumerations;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Authentication> GetLoginByCredentials(UserLogin login)
        {
            return await _unitOfWork.AuthenticationRepository.GetLoginByCredentials(login) ?? throw new BusinessException("El usuario no existe.");
        }

        public async Task RegisterUser(Authentication authentication, string currentUserRole)
        {
            if (currentUserRole != null)
            {
                if (currentUserRole.Equals(RoleType.Administrator))
                {
                    if (!authentication.UserRole.Equals(RoleType.Administrator))
                    {
                        throw new BusinessException("Un usuario con rol administrador solo puede crear usuarios administradores.");
                    }
                }
                else if (currentUserRole.Equals(RoleType.Organizer))
                {
                    if (!authentication.UserRole.Equals(RoleType.Organizer) || !authentication.UserRole.Equals(RoleType.Consumer))
                    {
                        throw new BusinessException("Un usuario organizador solo puede crear un usuario organizador o consumidor.");
                    }
                }
                else
                {
                    throw new BusinessException("No tienes permisos para crear un nuevo usuario.");
                }
            }
            else
            {
                if (!authentication.UserRole.Equals(RoleType.Organizer))
                {
                    throw new BusinessException("Un usuario sin registrar solo puede crear usuarios organizadores.");
                }
            }

            var exists = await _unitOfWork.AuthenticationRepository.GetLoginByCredentials(new UserLogin() { User = authentication.UserLogin });

            if (exists != null)
            {
                throw new BusinessException("El usuario ya existe.");
            }

            await _unitOfWork.AuthenticationRepository.Add(authentication);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
