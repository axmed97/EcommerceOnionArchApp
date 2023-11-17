using Application.Abstraction.Token;
using Application.DTOs;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var findUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            findUser ??= await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (findUser == null) throw new NotFoundUserException();

            var result = await _signInManager.PasswordSignInAsync(findUser, request.Password, request.RememberMe, true);
            
            if (result.Succeeded)
            {
                TokenDTO token = _tokenHandler.CreateAccessToken(30);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            throw new AuthErrorException();

        }
    }
}
