using Application.Abstraction.Services;
using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUser.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.RegisterAsync(new()
            {
                Email = request.Email,
                FullName = request.FullName,
                Username = request.Username,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
            });
            return new()
            {
                Message = response.Message,
                Success = response.Success
            };
        }
    }
}
