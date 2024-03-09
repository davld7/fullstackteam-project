using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Ioon.Infrastructure.Persistence
{
    public class DatabaseOptions
    {
        /// <summary>
        ///     Cadena de conexión al servidor.
        /// </summary>
        /// <seealso cref="ConnectionString" />
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        ///     Tipo de cadena de conexión del servidor.
        ///     Permite crear de forma correcta el proveedor de base de datos.
        /// </summary>
        /// <seealso cref="ConnectionType" />
        public string ConnectionType { get; set; } = string.Empty;

        /// <summary>
        ///     Método de fábrica que crea un proveedor de base de datos.
        /// </summary>
        /// <returns>
        ///     Un proveedor de base de datos:
        ///     <list type="bullet">
        ///         <item><see cref="SqlConnection" /></item>
        ///         <description>Proveedor SQL Server</description>
        ///     </list>
        /// </returns>
        public DbConnection CreateConnection()
        {
            return ConnectionType switch
            {
                nameof(SqlConnection) => new SqlConnection(ConnectionString),
                _ => throw new NotSupportedException("Proveedor de base de datos no soportado.")
            };
        }
    }
}
