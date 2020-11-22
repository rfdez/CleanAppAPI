using CleanApp.Core.Entities;
using CleanApp.Infrastructure.Repositories.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IYearRepository YearRepository { get; }

        IMonthRepository MonthRepository { get; }

        IWeekRepository WeekRepository { get; }

        ITenantRepository TenantRepository { get; }

        IRoomRepository RoomRepository { get; }

        IJobRepository JobRepository { get; }

        IAuthenticationRepository AuthenticationRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
