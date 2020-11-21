using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.Entities
{
    public partial class Tenant : BaseEntity
    {
        public Tenant()
        {
            Cleanlinesses = new HashSet<Cleanliness>();
        }

        public string TenantName { get; set; }

        public virtual ICollection<Cleanliness> Cleanlinesses { get; set; }
    }
}
