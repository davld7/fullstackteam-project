namespace Ioon.Domain.Common.Interfaces.Services
{
    /// <summary>
    /// Servicio para generar tokens JWT.
    /// </summary>
    public interface IJwtService
    {
        string GenerateToken(string UUID, string username, string email);
    }
}
