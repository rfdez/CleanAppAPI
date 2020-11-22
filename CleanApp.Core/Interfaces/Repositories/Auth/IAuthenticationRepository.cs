using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Interfaces;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories.Auth
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        Task<Authentication> GetLoginByCredentials(UserLogin login);
    }
}