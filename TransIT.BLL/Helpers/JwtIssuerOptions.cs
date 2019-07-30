using System;
using Microsoft.IdentityModel.Tokens;

namespace TransIT.BLL.Helpers
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public int AccessExpirationMins { get; set; }
        public int RefreshExpirationMins { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(AccessExpirationMins);
        public string JtiGenerator => Guid.NewGuid().ToString();
        public SigningCredentials SigningCredentials { get; set; }
    }
}
