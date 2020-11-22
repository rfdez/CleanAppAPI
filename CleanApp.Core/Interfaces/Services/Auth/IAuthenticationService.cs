using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using System.Threading.Tasks;

namespace CleanApp.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Authentication> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(Authentication authentication);
    }
}