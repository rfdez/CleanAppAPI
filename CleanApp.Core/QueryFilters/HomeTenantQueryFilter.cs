using CleanApp.Core.Enumerations;

namespace CleanApp.Core.QueryFilters
{
    public class HomeTenantQueryFilter : BaseQueryFilter
    {
        public int? HomeId { get; set; }
        public int? TenantId { get; set; }
        public TenantRole? TenantRole { get; set; }
    }
}
