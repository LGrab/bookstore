using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.UnitTests.ValueObjects;

public class IsbnTests
{
    [Theory]
    [InlineData("0306406152", "0306406152")]
    [InlineData("0-306-40615-2", "0306406152")]
    [InlineData(" 0 306 40615 2 ", "0306406152")]
    [InlineData("043942089x", "043942089X")]
    [InlineData("0-439-42089-X", "043942089X")]
    public void Constructor_WithValidIsbn10_ShouldCreateAndNormalizeIsbn(string rawIsbn, string expectedNormalized)
    {
        var isbn = new Isbn(rawIsbn);

        Assert.Equal(expectedNormalized, isbn.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("---")]
    [InlineData(null)]
    public void Constructor_WithEmptyOrNullIsbn_ShouldThrowArgumentException(string? invalidIsbn)
    {
        Assert.Throws<ArgumentException>(() => new Isbn(invalidIsbn!));
    }

    [Theory]
    [InlineData("030640615")]
    [InlineData("03064061521")]
    [InlineData("03064061A2")]
    [InlineData("030640615Y")]
    [InlineData("0306406153")] // Bad control sum
    public void Constructor_WithInvalidIsbn10FormatOrChecksum_ShouldThrowArgumentException(string invalidIsbn)
    {
        Assert.Throws<ArgumentException>(() => new Isbn(invalidIsbn));
    }

    [Fact]
    public void Equality_WithSameNormalizedIsbn_ShouldBeEqual()
    {
        var isbn1 = new Isbn("0-306-40615-2");
        var isbn2 = new Isbn("0306406152");

        Assert.Equal(isbn1, isbn2);
    }
}