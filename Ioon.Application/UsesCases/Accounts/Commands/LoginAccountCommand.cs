using MediatR;

namespace Ioon.Application.UsesCases.Accounts.Commands
{
    public record LoginAccountCommand(string Email, byte[] Password) : IRequest<ApplicationResponse>;
}
