namespace ExceleTech.Domain.Common
{
    public record Money(string Currency, decimal Amount)
    {
        public static Money operator *(Money money, int multiplier)
        {
            return new Money(money.Currency, money.Amount * multiplier);
        }
        public static Money operator *(int multiplier,Money money)
        {
            return new Money(money.Currency, money.Amount * multiplier);
        }
        public static Money operator /(Money money, int divider)
        {
            return new Money(money.Currency, money.Amount / divider);
        }
        public static Money operator +(Money money1, Money money2) 
        {
            if (money1.Currency != money2.Currency) return null;
            return new Money(money1.Currency, money1.Amount + money2.Amount);
        }
        public static Money operator -(Money money1, Money money2)
        {
            if (money1.Currency != money2.Currency) return null;
            return new Money(money1.Currency, money1.Amount - money2.Amount);

        }
    }
}
