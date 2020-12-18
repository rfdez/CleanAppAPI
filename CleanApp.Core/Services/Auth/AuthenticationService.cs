using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Enumerations;
using CleanApp.Core.Exceptions;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Services.Auth;
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
            var exists = await _unitOfWork.AuthenticationRepository.GetLoginByCredentials(new UserLogin() { User = authentication.UserLogin });

            if (exists != null)
            {
                throw new BusinessException("El usuario ya existe.");
            }

            if (!RoleType.Administrator.ToString().Equals(currentUserRole) && RoleType.Administrator.Equals(authentication.UserRole))
            {
                throw new BusinessException("No puede crear un usuario Administrador sin pertenecer al mismo.");
            }

            await _unitOfWork.AuthenticationRepository.Add(authentication);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
