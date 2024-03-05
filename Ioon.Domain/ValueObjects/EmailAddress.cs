using System.Text.RegularExpressions;

namespace Ioon.Domain.ValueObjects
{
    public partial record EmailAddress
    {
        private const string EmailRegex = @"^(?!\.)[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(?<!\.)$";

        public string Value { get; init; }

        private EmailAddress(string value) => Value = value;

        [GeneratedRegex(EmailRegex)]
        public static partial Regex EmailAddressRegex();

        public static EmailAddress? Create(string value)
        {
            if (string.IsNullOrEmpty(value) || !EmailAddressRegex().IsMatch(value))
            {
                return null;
            }

            return new EmailAddress(value.Trim());
        }
    }
}
