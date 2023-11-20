using Application.Features.Commands.AppUser.GoogleLogin;
using Application.Features.Commands.AppUser.LoginUser;
using Application.Features.Commands.AppUser.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommandRequest registerUserCommandRequest)
        {
            var response = await _mediator.Send(registerUserCommandRequest);
            return Ok(response);
        }
        
    }
}
