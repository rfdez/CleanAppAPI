using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.Entities
{
    public partial class Job : BaseEntity
    {
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
