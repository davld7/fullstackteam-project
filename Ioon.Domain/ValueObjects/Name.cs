using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial class Name
    {
        private const string PatternName = @"^([a-zA-ZáéíóúüÁÉÍÓÚÜñÑ ][0-9]*)$";

        private const int MaxLength = 50;

        private const int MinLength = 15;

        public string Value { get; init; }

        private Name(string value) => Value = value;

        [GeneratedRegex(PatternName)]
        private static partial Regex NameRegex();

        public static Name? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !NameRegex().IsMatch(value) || value.Length < MinLength || value.Length > MaxLength)
            {
                return null;
            }
            return new Name(value.Trim());
        }

    }
}
