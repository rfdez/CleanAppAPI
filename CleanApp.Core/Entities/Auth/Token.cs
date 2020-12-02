using System;

namespace CleanApp.Core.Entities.Auth
{
    public class Token
    {
        public string TokenType { get; set; } = "Bearer";

        public string AccessToken { get; set; }

        public string CreatedIn { get; set; }

        public string ExpiresIn { get; set; }
    }
}
