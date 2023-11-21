using Application.DTOs;
using Domain.Entities.Identity;

namespace Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(int seconds, AppUser appUser);
        string CreateRefreshToken();
    }
}
