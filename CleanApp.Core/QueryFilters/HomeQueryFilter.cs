using System.Collections.Generic;

namespace CleanApp.Core.QueryFilters
{
    public class HomeQueryFilter : BaseQueryFilter
    {
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeCountry { get; set; }
        public string HomeZipCode { get; set; }
        public int? TenantId { get; set; }
        public IEnumerable<int?> TenantIds { get; set; }
    }
}
