namespace CleanApp.Core.QueryFilters
{
    public class WeekQueryFilter : BaseQueryFilter
    {
        public int MonthId { get; set; }

        public int WeekValue { get; set; }
    }
}
