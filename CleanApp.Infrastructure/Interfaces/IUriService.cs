using CleanApp.Core.QueryFilters;
using System;

namespace CleanApp.Infrastructure.Services
{
    public interface IUriService
    {
        Uri GetMonthPaginationUri(MonthQueryFilter filters, string actionUrl);
        Uri GetWeekPaginationUri(WeekQueryFilter filters, string actionUrl);
        Uri GetTenantPaginationUri(TenantQueryFilter filters, string actionUrl);
    }
}