using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }

    public string Author { get; private set; }

    public Isbn Isbn { get; private set; }

    public int StockQuantity { get; private set; }


    public static Book Create(string title, string author, Isbn isbn, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }

        return new Book(Guid.NewGuid(), title, author, isbn, stockQuantity);
    }


    private Book(Guid id, string title, string author, Isbn isbn, int stockQuantity)
    {
        Id = id;
        Title = title;
        Author = author;
        Isbn = isbn;
        StockQuantity = stockQuantity;
    }
}