using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial class BusinessName
    {
        private const string PatternName = @"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ ][0-9]*)$";

        private const int MaxLength = 50; // Corregido el nombre de la variable

        private const int MinLength = 15; // Corregido el nombre de la variable

        public string Value { get; init; }

        private BusinessName(string value) => Value = value;

        [GeneratedRegex(PatternName)]
        private static partial Regex BusinessNameRegex();

        public static BusinessName? Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || !BusinessNameRegex().IsMatch(value) || value.Length < MinLength || value.Length > MaxLength)
            {
                return null;
            }

            return new BusinessName(value.Trim());

        }  
    }
}
