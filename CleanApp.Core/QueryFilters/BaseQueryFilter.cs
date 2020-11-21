using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.QueryFilters
{
    public abstract class BaseQueryFilter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
