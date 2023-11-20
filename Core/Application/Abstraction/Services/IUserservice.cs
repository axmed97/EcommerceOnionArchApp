using Application.DTOs.UserDTOs;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<RegisterUserResponseDTO> RegisterAsync(RegisterUserDTO registerUserDTO);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
