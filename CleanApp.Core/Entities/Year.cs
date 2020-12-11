using System.Collections.Generic;

namespace CleanApp.Core.Entities
{
    public partial class Year : BaseEntity
    {
        public Year()
        {
            Months = new HashSet<Month>();
        }

        public int YearValue { get; set; }

        public virtual ICollection<Month> Months { get; set; }
    }
}
