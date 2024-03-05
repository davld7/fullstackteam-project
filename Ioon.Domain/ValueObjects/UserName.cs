using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial class UserName
    {
        private const string PatternName = @"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ ]*)$";

        private const int MaxLength = 50; // Corregido el nombre de la variable

        private const int MinLength = 15; // Corregido el nombre de la variable

        public string Value { get; init; }

        private UserName(string value) => Value = value;

        [GeneratedRegex(PatternName)]
        private static partial Regex UserNameRegex();

        public static UserName? Create(string value)
        {
            if (string.IsNullOrEmpty(value) ||
                !UserNameRegex().IsMatch(value) || // Verifica que no coincida con el patrón regex
                value.Length < MinLength ||        // Verifica el tamaño mínimo
                value.Length > MaxLength)          // Verifica el tamaño máximo
            {
                return null;
            }

            return new UserName(value.Trim());
        }

    }
}
