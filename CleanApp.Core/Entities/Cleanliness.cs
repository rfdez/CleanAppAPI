using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.Entities
{
    public partial class Cleanliness
    {
        public int RoomId { get; set; }
        public int WeekId { get; set; }
        public int? TenantId { get; set; }
        public bool CleanlinessDone { get; set; }

        public virtual Room Room { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual Week Week { get; set; }
    }
}
