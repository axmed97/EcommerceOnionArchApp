using Application.Features.Commands.AppUser.GoogleLogin;
using Application.Features.Commands.AppUser.LoginUser;
using Application.Features.Commands.AppUser.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        { 
            var response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]/RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenLoginCommandRequest refreshTokenLoginCommand)
        {
            var response = await _mediator.Send(refreshTokenLoginCommand);
            return Ok(response);
        }
    }
}
