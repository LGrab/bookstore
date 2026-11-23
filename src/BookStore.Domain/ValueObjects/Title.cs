namespace BookStore.Domain.ValueObjects;

public sealed record Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Title cannot be null or whitespace.", nameof(value));
        }

        var trimmedTitle = value.Trim();

        if (trimmedTitle.Length > 200)
        {
            throw new ArgumentException("Title cannot be longer than 200 characters.", nameof(value));
        }

        Value = trimmedTitle;
    }
}