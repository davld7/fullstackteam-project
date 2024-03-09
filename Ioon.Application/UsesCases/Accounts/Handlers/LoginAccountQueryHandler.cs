using Ioon.Application.UsesCases.Accounts.Queries;
using Ioon.Domain.Common.Interfaces.Base;
using MediatR;

namespace Ioon.Application.UsesCases.Accounts.Handlers
{
    public class LoginAccountQueryHandler : IRequestHandler<LoginAccountQuery, ApplicationResponse>
    {
        private IDatabaseContext _Context;

        public LoginAccountQueryHandler(IDatabaseContext databaseContext)
        {
            _Context = databaseContext;
        }

        public async Task<ApplicationResponse> Handle(LoginAccountQuery request, CancellationToken cancellationToken)
        {
            return await 
        }
    }
}
