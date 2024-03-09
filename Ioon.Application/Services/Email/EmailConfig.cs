namespace Ioon.Application.Services.Email
{
    /// <summary>
    /// Representa la configuración para el servicio de correo electrónico.
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// Obtiene o establece la dirección del servidor de correo electrónico.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de puerto del servidor de correo electrónico.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de usuario para autenticarse con el servidor de correo electrónico.
        /// </summary>
        public string User { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el token para la verificación de correo electrónico.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el nombre de la empresa asociada con el servicio de correo electrónico.
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

    }
}
