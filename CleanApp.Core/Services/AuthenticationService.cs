using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
            return await _unitOfWork.AuthenticationRepository.GetLoginByCredentials(login);
        }

        public async Task RegisterUser(Authentication authentication)
        {
            await _unitOfWork.AuthenticationRepository.Add(authentication);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
