using CleanApp.Core.QueryFilters;
using System;

namespace CleanApp.Infrastructure.Services
{
    public interface IUriService
    {
        Uri GetMonthPaginationUri(MonthQueryFilter filter, string actionUrl);
    }
}