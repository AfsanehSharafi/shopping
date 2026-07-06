namespace Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } // مثلاً "IRR" یا "USD"

    private Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency must be specified.");

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public static Money Create(decimal amount, string currency) => new(amount, currency);

    // این متد، جادوی بیزینس است!
    public Money Add(Money other)
    {
        if (this.Currency != other.Currency)
        {
            throw new InvalidOperationException("Cannot add money with different currencies!");
        }

        return new Money(this.Amount + other.Amount, this.Currency);
    }
}
