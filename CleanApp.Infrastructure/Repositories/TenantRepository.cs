using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(CleanAppDDBBContext context) : base(context) { }

    }
}
