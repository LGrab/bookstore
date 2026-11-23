using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Author Author { get; private set; }
    public Isbn Isbn { get; private set; }
    public int StockQuantity { get; private set; }
    public Money Price { get; private set; }

    public static Book Create(Title title, Author author, Isbn isbn, int stockQuantity, Money price)
    {
        if (title == null) throw new ArgumentNullException(nameof(title), "Title required");
        if (author == null) throw new ArgumentNullException(nameof(author), "Author required");
        if (isbn == null) throw new ArgumentNullException(nameof(isbn), "Isbn required");
        if (price == null) throw new ArgumentNullException(nameof(price), "Price required");
        if (stockQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be less than 0.");

        return new Book(Guid.NewGuid(), title, author, isbn, stockQuantity, price);
    }

    private Book(Guid id, Title title, Author author, Isbn isbn, int stockQuantity, Money price)
    {
        Id = id;
        Title = title;
        Author = author;
        Isbn = isbn;
        StockQuantity = stockQuantity;
        Price = price;
    }
}