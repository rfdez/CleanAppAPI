using CleanApp.Core.QueryFilters;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace CleanApp.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public string GetCleanlinessFilterUri(CleanlinessQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "CleanlinessDone", filters.CleanlinessDone.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "RoomId", filters.RoomId.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetHomeFilterUri(HomeQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "HomeAddress", filters.HomeAddress.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "HomeCity", filters.HomeCity.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetHomeTenantFilterUri(HomeTenantQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "TenantRole", filters.TenantRole.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "TenantId", filters.TenantId.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetJobFilterUri(JobQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "JobName", filters.JobName.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "JobDescription", filters.JobDescription.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetMonthFilterUri(MonthQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "MonthValue", filters.MonthValue.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "YearId", filters.YearId.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public Uri GetPaginationUri(int pageNumer, int pageSize, string actionUrl)
        {
            var _endpointUri = new Uri(string.Concat(_baseUri, actionUrl));

            var pagedEndpointUri = QueryHelpers.AddQueryString(_endpointUri.ToString(), "PageNumber", pageNumer.ToString());
            pagedEndpointUri = QueryHelpers.AddQueryString(pagedEndpointUri, "PageSize", pageSize.ToString());

            return new Uri(pagedEndpointUri);
        }

        public string GetRoomFilterUri(RoomQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "RoomName", filters.RoomName.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetTenantFilterUri(TenantQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "TenantName", filters.TenantName.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetWeekFilterUri(WeekQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "MonthId", filters.MonthId.ToString());
            filteredUri = QueryHelpers.AddQueryString(filteredUri, "WeekValue", filters.WeekValue.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }

        public string GetYearFilterUri(YearQueryFilter filters)
        {
            string _uri = string.Empty;
            var filteredUri = QueryHelpers.AddQueryString(_uri, "YearValue", filters.YearValue.ToString());
            filteredUri = filteredUri.Replace("?", "&");
            return filteredUri;
        }
    }
}
