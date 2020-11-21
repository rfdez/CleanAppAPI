using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.Entities
{
    public partial class Month : BaseEntity
    {
        public Month()
        {
            Weeks = new HashSet<Week>();
        }

        public int MonthValue { get; set; }
        public int YearId { get; set; }

        public virtual Year Year { get; set; }
        public virtual ICollection<Week> Weeks { get; set; }
    }
}
