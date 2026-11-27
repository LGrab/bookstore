using BookStore.Domain.Entities;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.UnitTests.Entities;

public class BookTest
{
    private readonly Title _validTitle = new("Vražda v Orient expresu");
    private readonly Author _validAuthor = new("Agatha", "Christie");
    private readonly Isbn _validIsbn = new("0306406152");
    private readonly Money _validPrice = new(349m, "CZK");

    [Fact]
    public void Create_WithValidData_ShouldCreateNewBookWithId()
    {
        var stockQuantity = 10;

        var book = Book.Create(_validTitle, _validAuthor, _validIsbn, stockQuantity, _validPrice);

        Assert.NotEqual(Guid.Empty, book.Id);
        Assert.Equal(_validTitle, book.Title);
        Assert.Equal(_validAuthor, book.Author);
        Assert.Equal(_validIsbn, book.Isbn);
        Assert.Equal(stockQuantity, book.StockQuantity);
        Assert.Equal(_validPrice, book.Price);
    }
    
    [Fact]
    public void Create_Twice_ShouldGenerateDifferentIds()
    {
        var firstBook = Book.Create(
            _validTitle, _validAuthor, _validIsbn, 5, _validPrice);

        var secondBook = Book.Create(
            _validTitle, _validAuthor, _validIsbn, 5, _validPrice);

        Assert.NotEqual(firstBook.Id, secondBook.Id);
    }

    [Fact]
    public void Create_WithZeroStockQuantity_ShouldCreateBook()
    {
        var book = Book.Create(_validTitle, _validAuthor, _validIsbn, 0, _validPrice);

        Assert.Equal(0, book.StockQuantity);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Create_WithNegativeStockQuantity_ShouldThrowArgumentOutOfRangeException(int negativeStock)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Book.Create(_validTitle, _validAuthor, _validIsbn, negativeStock, _validPrice));
    }

    [Fact]
    public void Create_WithNullTitle_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Book.Create(null!, _validAuthor, _validIsbn, 5, _validPrice));
    }

    [Fact]
    public void Create_WithNullAuthor_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Book.Create(_validTitle, null!, _validIsbn, 5, _validPrice));
    }

    [Fact]
    public void Create_WithNullIsbn_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Book.Create(_validTitle, _validAuthor, null!, 5, _validPrice));
    }

    [Fact]
    public void Create_WithNullPrice_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Book.Create(_validTitle, _validAuthor, _validIsbn, 5, null!));
    }
}