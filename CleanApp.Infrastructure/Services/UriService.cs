using CleanApp.Core.QueryFilters;
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

        public Uri GetMonthPaginationUri(MonthQueryFilter filters, string actionUrl)
        {
            string uri = $"{_baseUri}{actionUrl}";

            return new Uri(uri);
        }
        
        public Uri GetWeekPaginationUri(WeekQueryFilter filters, string actionUrl)
        {
            string uri = $"{_baseUri}{actionUrl}";

            return new Uri(uri);
        }
    }
}
