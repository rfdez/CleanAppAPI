using CleanApp.Core.QueryFilters;
using System;

namespace CleanApp.Infrastructure.Services
{
    public interface IUriService
    {
        Uri GetPaginationUri(int pageNumer, int pageSize, string actionUrl);
        string GetMonthFilterUri(MonthQueryFilter filters);
        string GetWeekFilterUri(WeekQueryFilter filters);
        string GetTenantFilterUri(TenantQueryFilter filters);
        string GetRoomFilterUri(RoomQueryFilter filters);
        string GetHomeFilterUri(HomeQueryFilter filters);
        string GetJobFilterUri(JobQueryFilter filters);
        string GetHomeTenantFilterUri(HomeTenantQueryFilter filters);
        string GetCleanlinessFilterUri(CleanlinessQueryFilter filters);
    }
}