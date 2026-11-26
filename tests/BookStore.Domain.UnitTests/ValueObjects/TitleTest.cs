using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.UnitTests.ValueObjects;

public class TitleTest
{
    [Fact]
    public void Constructor_WithValidTitle_ShouldCreateTitleAndTrimWhitespace()
    {
        var rawTitle = "   Harry Potter   ";

        var title = new Title(rawTitle);

        Assert.Equal("Harry Potter", title.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithNullOrWhitespace_ShouldThrowArgumentException(string? invalidTitle)
    {
        Assert.Throws<ArgumentException>(() => new Title(invalidTitle!));
    }

    [Fact]
    public void Constructor_WithLongerTitleThan200Chars_ShouldThrowArgumentException()
    {
        var tooLongTitle = new string('x', 201);
        
        Assert.Throws<ArgumentException>(() => new Title(tooLongTitle));
    }
}