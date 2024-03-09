using MediatR;

namespace Ioon.Application.UsesCases.Users.Commands
{
    public record LoginUserCommand(string Email, byte[] Password) : IRequest<ApplicationResponse>;
}
