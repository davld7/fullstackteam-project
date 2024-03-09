namespace Ioon.Domain.ValueObjects
{
    public partial class Money
    {
        public decimal Value { get; init; }

        private Money(decimal value) => Value = value;

        public static Money? Create(decimal value)
        {
            if(value < 0 || value > 99999.99m)
            {
                return null;
            }
            return new Money(value);
        }
    }
}
