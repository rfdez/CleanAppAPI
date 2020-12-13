using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using System.Threading.Tasks;

namespace CleanApp.Core.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<Authentication> GetLoginByCredentials(UserLogin login);
        Task RegisterUser(Authentication authentication, string currentUserRole);
    }
}