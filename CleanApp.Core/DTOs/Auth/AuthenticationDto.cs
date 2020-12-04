using CleanApp.Core.Enumerations;

namespace CleanApp.Core.DTOs
{
    public class AuthenticationDto
    {
        public int Id { get; set; }

        public string UserLogin { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public RoleType UserRole { get; set; } = RoleType.Organizer;
    }
}
