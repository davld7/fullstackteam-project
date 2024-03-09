using Ioon.Application.UsesCases.Users.Commands;
using static Ioon.Application.Extensions.HandlerExtensions;
using Ioon.Domain.Common.Interfaces.Repositories;
using Ioon.Domain.Common.Interfaces.Services;
using MediatR;
using Ioon.Domain.Common.Enums;
using Ioon.Domain;
using Ioon.Application.Common.DTO;

namespace Ioon.Application.UsesCases.Users.Handlers
{
    public sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, ApplicationResponse>
    {
        private readonly IHasherService _hashService;
        private readonly IUserRepository<User, AccountDTO> _userRepository;

        public LoginUserHandler(IHasherService hashService, IUserRepository<User, AccountDTO> userRepository)
        {
            _hashService = hashService;
            _userRepository = userRepository;
        }

        public async Task<ApplicationResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var useInfo = await _userRepository.GetInfoUserAsync(request.Email);

            if (useInfo.UserId == Guid.Empty)
            {
                return BuildResponse(AuthStatus.UserNotFound);
            }

            bool isValid = _hashService.VerifyPassword(request.Password, useInfo.HashPassword, useInfo.HashSalt);

            if (!isValid)
            {
                return BuildResponse(AuthStatus.InvalidPassword);
            }

            var dataAccount = await _userRepository.GetDataAccountAsync(useInfo.UserId);

            return BuildResponse(AuthStatus.UserAuthorized, dataAccount);

        }

    }
}
