using CleanApp.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.DTOs
{
    public class AuthenticationDto
    {
        public int Id { get; set; }
        public string CurrentUser { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public RoleType UserRole { get; set; } = RoleType.Consumer;
    }
}
