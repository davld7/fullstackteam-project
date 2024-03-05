using Ioon.Application.Common.DTO;
using MediatR;

namespace Ioon.Application.UsesCases.Users.Commands
{
    public record CreateUserCommand(
        string BusinessId,
        string FullName,
        string EmailAddress,
        string PhoneNumber,
        byte[] Password,
        string Identification,
        string RoleId
    ) : IRequest<ApplicationResponse>;

}
