namespace CleanApp.Core.QueryFilters
{
    public class JobQueryFilter : BaseQueryFilter
    {
        public int RoomId { get; set; }

        public string JobName { get; set; }

        public string JobDescription { get; set; }
    }
}
