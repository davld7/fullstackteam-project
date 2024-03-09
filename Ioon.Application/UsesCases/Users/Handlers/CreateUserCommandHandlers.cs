using static Ioon.Application.Extensions.HandlerExtensions;
using Ioon.Domain;
using Ioon.Domain.Common.Enums;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Common.Interfaces.Repositories;
using Ioon.Domain.Common.Interfaces.Services;
using Ioon.Domain.ValueObjects;
using MediatR;
using Ioon.Application.UsesCases.Users.Commands;

namespace Ioon.Application.UsesCases.Users.Handlers
{
    internal sealed class CreateUserCommandHandlers : IRequestHandler<CreateUserCommand, ApplicationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasherService _hashService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandlers(IUserRepository userRepository, IHasherService hashService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ApplicationResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (Name.Create(request.FullName) is not Name userName)
                {                  
                    return BuildResponse(EntityUserStatus.InvalidUserName);
                }

                if (EmailAddress.Create(request.EmailAddress) is not EmailAddress emailAddress)
                {
                    return BuildResponse(EntityUserStatus.InvalidEmailAddress);
                }

                if (Identification.Create(request.Identification) is not Identification identification)
                {
                    return BuildResponse(EntityUserStatus.InvalidIdentification);
                }

                if (PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
                {
                    return BuildResponse(EntityUserStatus.InvalidPhoneNumber);
                }

                await _userRepository.VerifyUser(request.EmailAddress, request.Identification);
                var (Hash, Salt) = _hashService.HashPassword(request.Password);

                var user = BuildUserEntity(request, userName, emailAddress, phoneNumber, identification, Hash, Salt);

                await _userRepository.RegisterUserAsync(user);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);


                return BuildResponse(EntityUserStatus.UserCreated);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        private User BuildUserEntity(CreateUserCommand request, Name userName, EmailAddress emailAddress, PhoneNumber phoneNumber, Identification identification, byte[] Hash, byte[] Salt)
        {
            return new User(
                Guid.NewGuid(),
                Guid.Parse(request.BusinessId),
                userName,
                emailAddress,
                Hash,
                Salt,
                phoneNumber,
                identification,
                Guid.Parse(request.RoleId)            
           );
        }

    }
}
