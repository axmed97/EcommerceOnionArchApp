using Application.Abstraction.Services;
using Application.DTOs.UserDTOs;
using Application.Exceptions;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public async Task<RegisterUserResponseDTO> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            Domain.Entities.Identity.AppUser appUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerUserDTO.Username,
                FullName = registerUserDTO.FullName,
                Email = registerUserDTO.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerUserDTO.Password);

            RegisterUserResponseDTO response = new() { Success = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "User Created Successfully";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            }
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if(user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiredDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }
    }
}
