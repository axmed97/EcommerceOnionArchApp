using Application.Abstraction.Services;
using Application.Abstraction.Token;
using Application.DTOs;
using Application.Exceptions;
using Application.Features.Commands.AppUser.LoginUser;
using Azure.Core;
using Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        private async Task<TokenDTO> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        FullName = name,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);
                var token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 350);
                return token;
            }
            else
                throw new Exception("Invalid External Auth");
        }

        public async Task<TokenDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenDTO> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["ExternalLoginSettings:Google:App_Id"] },
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
        }

        public async Task<TokenDTO> LoginAsync(string UsernameOrEmail, string Password, int accessTokenLifeTime)
        {
            var findUser = await _userManager.FindByNameAsync(UsernameOrEmail);
            findUser ??= await _userManager.FindByEmailAsync(UsernameOrEmail);
            if (findUser == null) throw new NotFoundUserException();
            var result = await _signInManager.CheckPasswordSignInAsync(findUser, Password, true);

            if (result.Succeeded)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                await _userService.UpdateRefreshToken(token.RefreshToken, findUser, token.Expiration, 350);
                return token;
            }
            throw new AuthErrorException();
        }

        public async Task<TokenDTO> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if(user != null && user?.RefreshTokenExpiredDate > DateTime.UtcNow)
            {
                var token = _tokenHandler.CreateAccessToken(150);
                await _userService.UpdateRefreshToken(refreshToken, user, token.Expiration, 150);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
