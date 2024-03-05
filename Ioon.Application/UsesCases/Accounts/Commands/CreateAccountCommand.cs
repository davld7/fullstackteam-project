using Ioon.Application.Common.DTO;
using MediatR;

namespace Ioon.Application.UsesCases.Accounts.Commands
{
    public record CreateAccountCommand(
        string BusinessName,
        string EmailAddressBusiness,
        string PhoneNumberBusiness,
        string Address,
        string ImgUrl,
        string Ruc,
        string DepartmentId,
        string BusinessTypeId,
        string FullNameOwner,
        string EmailAddressOwner,
        string PhoneNumberOwner,
        byte[] Password,
        string Identification
    ) : IRequest<ApplicationResponse>;

}
