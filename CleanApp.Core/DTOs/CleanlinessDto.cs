using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.DTOs
{
    public class CleanlinessDto
    {
        public int RoomId { get; set; }
        public int WeekId { get; set; }
        public int TenantId { get; set; }
        public bool? CleanlinessDone { get; set; }
    }
}
