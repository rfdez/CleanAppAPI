using System;

namespace CleanApp.Core.Entities.Auth
{
    public class Token
    {
        public string TokenType { get; set; } = "Bearer";

        public string AccessToken { get; set; }

        public DateTime CreatedIn { get; set; }

        public DateTime ExpiresIn { get; set; }
    }
}
