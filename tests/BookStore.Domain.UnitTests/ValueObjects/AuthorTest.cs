using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.UnitTests.ValueObjects;

public class AuthorTest
{
    [Fact]
    public void Constructor_WithValidFirstNameAndLastName_ShouldCreateAuthorAndTrimNames()
    {
        var rawFirstName = "   Agatha    ";
        var rawLastName = "   Christie    ";

        var author = new Author(rawFirstName, rawLastName);

        Assert.Equal("Agatha", author.FirstName);
        Assert.Equal("Christie", author.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithInvalidFirstName_ShouldThrowArgumentException(string? invalidFirstName)
    {
        Assert.Throws<ArgumentException>(() => new Author(invalidFirstName!, "Christie"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithInvalidLastName_ShouldThrowArgumentException(string? invalidLastName)
    {
        Assert.Throws<ArgumentException>(() => new Author("Agatha", invalidLastName!));
    }

    [Fact]
    public void Constructor_WithFirstNameLongerThan100Chars_ShouldThrowArgumentException()
    {
        var tooLongFirstName = new string('x', 101);

        Assert.Throws<ArgumentException>(() => new Author(tooLongFirstName, "Christie"));
    }

    [Fact]
    public void Constructor_WithLastNameLongerThan100Chars_ShouldThrowArgumentException()
    {
        var tooLongLastName = new string('x', 101);

        Assert.Throws<ArgumentException>(() => new Author("Agatha", tooLongLastName));
    }

    [Fact]
    public void Constructor_WithFirstNameLong100Chars_ShouldCreateAuthor()
    {
        var exactLongFirstName = new string('x', 100);

        var author = new Author(exactLongFirstName, "Christie");
        Assert.Equal(exactLongFirstName, author.FirstName);
    }

    [Fact]
    public void Constructor_WithLastNameLong100Chars_ShouldCreateAuthor()
    {
        var exactLongLastName = new string('x', 100);

        var author = new Author("Agatha", exactLongLastName);
        Assert.Equal(exactLongLastName, author.LastName);
    }
}