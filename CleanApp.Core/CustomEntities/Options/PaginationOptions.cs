namespace CleanApp.Infrastructure.Options
{
    public class PaginationOptions
    {
        public int DefaultPageNumber { get; set; }

        public int DefaultPageSize { get; set; }

        public int MonthPageSize { get; set; }

        public int WeekPageSize { get; set; }
    }
}
