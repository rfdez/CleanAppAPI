﻿using CleanApp.Core.QueryFilters;
using System;

namespace CleanApp.Infrastructure.Services
{
    public interface IUriService
    {
        Uri GetMonthPaginationUri(MonthQueryFilter filters, string actionUrl);
        Uri GetWeekPaginationUri(WeekQueryFilter filters, string actionUrl);
        Uri GetTenantPaginationUri(TenantQueryFilter filters, string actionUrl);
        Uri GetRoomPaginationUri(RoomQueryFilter filters, string actionUrl);
        Uri GetHomePaginationUri(HomeQueryFilter filters, string actionUrl);
        Uri GetJobPaginationUri(JobQueryFilter filters, string actionUrl);
        Uri GetHomeTenantPaginationUri(HomeTenantQueryFilter filters, string actionUrl);
    }
}