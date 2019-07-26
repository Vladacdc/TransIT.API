using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Helpers.Abstractions
{
    /// <summary>
    /// Behavior of jwt
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// Exctracts user info from token
        /// </summary>
        /// <param name="token">Token string</param>
        /// <returns>User info</returns>
        (ClaimsPrincipal principal, JwtSecurityToken jwt) GetPrincipalFromExpiredToken(string token);
        /// <summary>
        /// Generates new token
        /// </summary>
        /// <param name="login">Login of user in token</param>
        /// <param name="role">Role of user in token</param>
        /// <returns>Entity token</returns>
        TokenDTO GenerateToken(string userId, string login, string role);
    }
}
