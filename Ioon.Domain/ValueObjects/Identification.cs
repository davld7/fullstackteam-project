using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial record Identification
    {
        private const string PatternRegex = @"^\d{3}-\d{6}-\d{4}[A-Za-z]$";

        private const int Length = 16;

        public string Value { get; init; }

        private Identification(string value) => Value = value;

        [GeneratedRegex(PatternRegex)]
        private static partial Regex IdentificationRegex();

        public static Identification? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !IdentificationRegex().IsMatch(value) || value.Length != Length)
            {
                return null;
            }

            return new Identification(value.Trim());
        }

    }
}
