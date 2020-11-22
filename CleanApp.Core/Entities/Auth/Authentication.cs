using CleanApp.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanApp.Core.Entities.Auth
{
    public class Authentication : BaseEntity
    {
        public string CurrentUser { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; } 
        public RoleType UserRole { get; set; }
    }
}
