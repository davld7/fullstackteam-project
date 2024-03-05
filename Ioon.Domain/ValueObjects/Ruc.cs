using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial class Ruc
    {
        private const int Length = 14;

        public string Value { get; init; }

        private static readonly Dictionary<string, string> RucRegexDictionary = new Dictionary<string, string>
        {
            { "NC", @"^\d{3}\d{6}\d{4}[A-Za-z]$$" },
            { "N", "^N[0-9]{13}$" },
            { "RE", "^[RE][0-9]{13}$" },
            { "J", "^J[0-9]{13}$" }
        };

        private Ruc(string value) => Value = value;

        public static Ruc? Create(string ruc)
        {
            if (string.IsNullOrWhiteSpace(ruc) || ruc.Length != Length)
            {
                return null;
            }
            foreach (var kvp in RucRegexDictionary)
            {
                if (Regex.IsMatch(ruc, kvp.Value))
                {
                    return new Ruc(ruc);
                }
            }
            return null; // Si el RUC no coincide con ningún patrón, retornamos null
        }

    }
}
