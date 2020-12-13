using System.Collections.Generic;

namespace CleanApp.Core.Entities
{
    public partial class Home : BaseEntity
    {
        public Home()
        {
            Tenants = new HashSet<HomeTenant>();
            Rooms = new HashSet<Room>();
        }

        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeCountry { get; set; }
        public string HomeZipCode { get; set; }

        public virtual ICollection<HomeTenant> Tenants { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
