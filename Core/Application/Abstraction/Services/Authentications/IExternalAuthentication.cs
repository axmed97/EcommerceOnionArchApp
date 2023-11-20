using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<TokenDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<TokenDTO> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
