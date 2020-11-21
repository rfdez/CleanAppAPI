using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.DTOs
{
    public class JobDto
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public int RoomId { get; set; }
    }
}
