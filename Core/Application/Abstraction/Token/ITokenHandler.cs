using Application.DTOs;

namespace Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        TokenDTO CreateAccessToken(int minutes);
    }
}
