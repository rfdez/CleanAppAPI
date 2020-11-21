using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.QueryFilters
{
    public class MonthQueryFilter : BaseQueryFilter
    {
        public int? YearId { get; set; }
        public int? MonthValue { get; set; }
    }
}
