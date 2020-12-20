namespace CleanApp.Core.QueryFilters
{
    public class TenantQueryFilter : BaseQueryFilter
    {
        public string AuthUser { get; set; }
        public string TenantName { get; set; }
    }
}
