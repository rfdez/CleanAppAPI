using CleanApp.Core.Enumerations;

namespace CleanApp.Core.Entities.Auth
{
    public class Authentication : BaseEntity
    {
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public RoleType UserRole { get; set; }
    }
}
