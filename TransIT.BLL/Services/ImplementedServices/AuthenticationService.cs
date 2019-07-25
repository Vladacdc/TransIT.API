using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using TransIT.BLL.Helpers.Abstractions;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactory _jwtFactory;
        
        public AuthenticationService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<AuthenticationService> logger,
            IJwtFactory jwtFactory,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _jwtFactory = jwtFactory;
        }
        
        public async Task<TokenDTO> SignInAsync(LoginDTO credentials)
        {
            try
            {

                var user = (await _userManager.FindByNameAsync(credentials.Login));

                if (user != null && (bool)user.IsActive && (await _userManager.CheckPasswordAsync(user, credentials.Password)))
                {
                    var role = (await _userManager.GetRolesAsync(user)).SingleOrDefault();
                    var token = _jwtFactory.GenerateToken(user.Id, user.UserName, role);
                    await _unitOfWork.SaveAsync();
                    return token;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(SignInAsync));
                throw e;
            }
        }

        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            try
            {
                var user = await _unitOfWork.UserManager.FindByIdAsync(
                        _jwtFactory.GetPrincipalFromExpiredToken(token.AccessToken).jwt.Subject
                        );
                var role = (await _userManager.GetRolesAsync(user)).SingleOrDefault();
                var newToken = _jwtFactory.GenerateToken(user.Id, user.UserName, role);

                await _unitOfWork.TokenRepository.AddAsync(new Token
                {
                    RefreshToken = newToken.RefreshToken,
                    CreatedById = user.Id,
                    Create = user
                });
                await _unitOfWork.SaveAsync();

                return null;
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
                throw e;
            }
        }
    }
}
