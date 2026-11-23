namespace BookStore.Domain.ValueObjects;

public sealed record Isbn
{
    public string Value { get; }

    public Isbn(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Isbn cannot be empty.", nameof(value));
        }

        var cleaned = value.Replace("-", "").Replace(" ", "").ToUpperInvariant();

        if (!IsValidIsbn10(cleaned))
            throw new ArgumentException("Invalid ISBN-10 format.", nameof(value));

        Value = cleaned;
    }

    private static bool IsValidIsbn10(string isbn)
    {
        if (isbn.Length != 10 || !isbn[..9].All(char.IsDigit))
            return false;

        char last = isbn[9];
        if (!char.IsDigit(last) && last != 'X')
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (isbn[i] - '0') * (10 - i);

        int check = last == 'X' ? 10 : last - '0';
        sum += check;

        return sum % 11 == 0;
    }
}