using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TransIT.BLL.Helpers.Abstractions;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Helpers
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }
        
        public (ClaimsPrincipal principal, JwtSecurityToken jwt) GetPrincipalFromExpiredToken(string token)
        {
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _jwtOptions.SigningCredentials.Key,
                        ValidateLifetime = false
                    },
                    out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            
            return (principal, jwtSecurityToken);
        }


        private string GenerateAccessToken(string userId, string login, string role) =>
            new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, login, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.AccessExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
                ));
        
        private string GenerateRefreshToken(string userId, string login, string role) =>
            new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: GenerateClaims(userId, login, role),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtOptions.RefreshExpirationMins)),
                    signingCredentials: _jwtOptions.SigningCredentials
            ));
        
        public TokenDTO GenerateToken(string userId, string login, string role) =>
            new TokenDTO
            {
                AccessToken = GenerateAccessToken(userId, login, role),
                RefreshToken = GenerateRefreshToken(userId, login, role)
            };

        private Claim[] GenerateClaims(string userId, string login, string role) =>
            new[]
            {
                new Claim(nameof(login), login),
                new Claim(nameof(role), role),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };
        
        private long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round(
                (date.ToUniversalTime() - new DateTimeOffset(
                     1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}
