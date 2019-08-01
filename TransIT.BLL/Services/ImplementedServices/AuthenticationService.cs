using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactory _jwtFactory;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            IJwtFactory jwtFactory,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _jwtFactory = jwtFactory;
        }

        public async Task<TokenDTO> SignInAsync(LoginDTO credentials)
        {
            try
            {
                var user = (await _unitOfWork.UserManager.FindByNameAsync(credentials.Login));

                if (user != null && (bool)user.IsActive && (await _unitOfWork.UserManager.CheckPasswordAsync(user, credentials.Password)))
                {
                    var role = (await _unitOfWork.UserManager.GetRolesAsync(user)).SingleOrDefault();
                    var token = _jwtFactory.GenerateToken(user.Id, user.UserName, role); 
                    return token;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SignInAsync));
                throw;
            }
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            try
            {
                var user = await _unitOfWork.UserManager.FindByIdAsync(
                        _jwtFactory.GetPrincipalFromExpiredToken(token.AccessToken).jwt.Subject
                        );
                var role = (await _unitOfWork.UserManager.GetRolesAsync(user)).SingleOrDefault();
                var newToken = _jwtFactory.GenerateToken(user.Id, user.UserName, role);

                return newToken;
            }
            catch (Exception e)
                when (e is SecurityTokenException || e is DbUpdateException)
            {
                _logger.LogError(e, nameof(TokenAsync));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(TokenAsync));
                throw;
            }
        }
    }
}