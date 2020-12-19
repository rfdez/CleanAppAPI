namespace CleanApp.Core.QueryFilters
{
    public class CleanlinessQueryFilter : BaseQueryFilter
    {
        public int? RoomId { get; set; }

        public int? WeekId { get; set; }

        public int? TenantId { get; set; }

        public bool? CleanlinessDone { get; set; }
    }
}
