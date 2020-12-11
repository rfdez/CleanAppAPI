using System.Collections.Generic;

namespace CleanApp.Core.Entities
{
    public partial class Room : BaseEntity
    {
        public Room()
        {
            Cleanlinesses = new HashSet<Cleanliness>();
            Jobs = new HashSet<Job>();
        }

        public string RoomName { get; set; }

        public virtual ICollection<Cleanliness> Cleanlinesses { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
