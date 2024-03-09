using Ioon.Domain.Common.Interfaces.Base;

namespace Ioon.Application.Common.DTO
{
    public class AccountDTO : IModelResponse
    {
        public Guid User { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid Business { get; set; }
        public string BusinessName { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public string CommerceToken { get; set; } = string.Empty;
        public string ServiceToken { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
