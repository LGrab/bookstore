namespace BookStore.Domain.ValueObjects;

public sealed record Author
{
    public string FirstName { get; }
    public string LastName { get; }

    public Author(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty", nameof(lastName));
        }

        var trimmedFirst = firstName.Trim();
        var trimmedLast = lastName.Trim();

        if (trimmedFirst.Length > 100)
        {
            throw new ArgumentException("First name cannot exceed 100 characters", nameof(firstName));
        }

        if (trimmedLast.Length > 100)
        {
            throw new ArgumentException("Last name cannot exceed 100 characters", nameof(lastName));
        }

        FirstName = trimmedFirst;
        LastName = trimmedLast;
    }
}