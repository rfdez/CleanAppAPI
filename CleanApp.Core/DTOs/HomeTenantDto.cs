using CleanApp.Core.Enumerations;

namespace CleanApp.Core.DTOs
{
    public class HomeTenantDto
    {
        public int HomeId { get; set; }
        public int TenantId { get; set; }
        public TenantRole TenantRole { get; set; }
    }
}
