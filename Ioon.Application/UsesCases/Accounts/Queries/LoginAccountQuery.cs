using MediatR;

namespace Ioon.Application.UsesCases.Accounts.Queries
{
    public record LoginAccountQuery(string Email, byte[] Password) : IRequest<ApplicationResponse>;
}
