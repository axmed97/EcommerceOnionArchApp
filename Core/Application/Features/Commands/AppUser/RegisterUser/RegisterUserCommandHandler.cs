using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUser.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public RegisterUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser appUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                FullName = request.FullName,
                Email = request.Email,
            };

            IdentityResult result =  await _userManager.CreateAsync(appUser, request.Password);

            RegisterUserCommandResponse response = new() { Success = result.Succeeded };

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
    }
}
