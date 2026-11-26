namespace BookStore.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Price cannot be less than 0.", nameof(amount));
        }

        if (decimal.Round(amount, 2) != amount)
        {
            throw new ArgumentException("Amount cannot have more than 2 decimal places.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be empty.", nameof(currency));
        }

        var normalizedCurrency = currency.Trim().ToUpperInvariant();

        if (normalizedCurrency.Length != 3)
        {
            throw new ArgumentException("Currency must be a 3-letter ISO code (e.g., CZK, EUR).", nameof(currency));
        }

        Amount = amount;
        Currency = normalizedCurrency;
    }
}