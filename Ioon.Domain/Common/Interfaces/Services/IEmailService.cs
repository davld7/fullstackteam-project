namespace Ioon.Domain.Common.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string destination);
        Task<string> SendEmailCodeVerificationAsync(string destination);
        Task SendPasswordResetAsync(string destination);    
    }
}
