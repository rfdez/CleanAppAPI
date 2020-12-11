using System.Collections.Generic;

namespace CleanApp.Core.Entities
{
    public partial class Week : BaseEntity
    {
        public Week()
        {
            Cleanlinesses = new HashSet<Cleanliness>();
        }

        public int WeekValue { get; set; }
        public int MonthId { get; set; }

        public virtual Month Month { get; set; }
        public virtual ICollection<Cleanliness> Cleanlinesses { get; set; }
    }
}
