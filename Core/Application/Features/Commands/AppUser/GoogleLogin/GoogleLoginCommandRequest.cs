using MediatR;

namespace Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandRequest : IRequest<GoogleLoginCommandResponse>
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}
