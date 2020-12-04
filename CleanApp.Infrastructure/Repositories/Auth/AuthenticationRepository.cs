using CleanApp.Infrastructure.Data;
using CleanApp.Core.Entities.Auth;
using System.Threading.Tasks;
using CleanApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanApp.Infrastructure.Repositories.Auth
{
    public class AuthenticationRepository : BaseRepository<Authentication>, IAuthenticationRepository
    {
        public AuthenticationRepository(CleanAppDDBBContext context) : base(context) { }

        public async Task<Authentication> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(u => u.UserLogin == login.User);
        }
    }
}
