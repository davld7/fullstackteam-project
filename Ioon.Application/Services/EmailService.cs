using Ioon.Domain.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Ioon.Application.Services
{
    public class EmailService : IEmailService
    {
        protected readonly IConfiguration _configuration;
        protected readonly string Host;
        protected readonly int Port;
        protected readonly string User;
        protected readonly string TokenVerification;
        protected static readonly Random random = new Random((int)DateTime.Now.Ticks);

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            Host = _configuration["Email:Host"] ?? throw new ArgumentNullException(nameof(Host));
            Port = _configuration.GetValue<int>("Email:Port");
            User = _configuration["Email:User"] ?? throw new ArgumentNullException(nameof(User));
            TokenVerification = _configuration["Email:Token"] ?? throw new ArgumentNullException(nameof(TokenVerification));
        }

        /// <summary>
        /// Envía un correo electrónico de verificación.
        /// </summary>
        /// <param name="destination">Destinatario del correo electrónico.</param>
        public async Task SendEmailVerificationAsync(string destination)
        {
            var message = MessageFactory(EmailType.Verify, destination, "Verifica tu cuenta de IOON");
            await SendEmailAsync(message);
        }

        /// <summary>
        /// Envía un correo electrónico de verificación con un código generado.
        /// </summary>
        /// <param name="destination">Destinatario del correo electrónico.</param>
        /// <returns>El código de verificación generado.</returns>
        public async Task<string> SendEmailCodeVerificationAsync(string destination)
        {
            string verificationCode = random.Next(100000, 999999).ToString(); ;
            var message = MessageFactory(EmailType.Verify, destination, "Codigo de verificación IOON", verificationCode);
            await SendEmailAsync(message);
            return verificationCode;
        }

        /// <summary>
        /// Envía un correo electrónico para restablecer la contraseña.
        /// </summary>
        /// <param name="destination">Destinatario del correo electrónico.</param>
        public async Task SendPasswordResetAsync(string destination)
        {
            var message = MessageFactory(EmailType.Reset, destination, "Restablece tu contraseña de IOON");
            await SendEmailAsync(message);
        }

        private MimeMessage MessageFactory(EmailType emailType, string destination, string subject, string? content = default)
        {
            var bodyMessage = new MimeMessage();

            bodyMessage.From.Add(new MailboxAddress("Ioon", User));
            bodyMessage.To.Add(MailboxAddress.Parse(destination));
            bodyMessage.Subject = subject;
            bodyMessage.Body = new TextPart(TextFormat.Html) { Text = GetHtmlBody(emailType, content ?? string.Empty) };

            return bodyMessage;
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.CheckCertificateRevocation = false;
                    await smtp.ConnectAsync(Host, Port, SecureSocketOptions.Auto);
                    await smtp.AuthenticateAsync(User, TokenVerification);
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al enviar el correo electrónico.", ex);
                }
            }
        }

        private string GetHtmlBody(EmailType emailType, string? content = default)
        {
            string template = emailType switch
            {
                EmailType.Verify => Properties.Resources.verify_email_template,
                EmailType.Reset => Properties.Resources.reset_password_template,
                _ => throw new NotImplementedException(),
            };
            return content is null ? template : template.Replace("{{}}", content);
        }
    }

    public enum EmailType
    {
        Verify,
        VerifyCode,
        Reset
    }
}
