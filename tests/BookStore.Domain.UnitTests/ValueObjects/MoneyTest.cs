using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.UnitTests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Constructor_WithValidAmountAndCurrency_ShouldCreateMoneyAndNormalizeCurrency()
    {
        var amount = 299.90m;
        var rawCurrency = "  czk  ";

        var money = new Money(amount, rawCurrency);

        Assert.Equal(299.90m, money.Amount);
        Assert.Equal("CZK", money.Currency);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(0.01)]
    [InlineData(100.55)]
    public void Constructor_WithBoundaryValidAmounts_ShouldCreateMoney(decimal validAmount)
    {
        var money = new Money(validAmount, "EUR");

        Assert.Equal(validAmount, money.Amount);
        Assert.Equal("EUR", money.Currency);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-100)]
    public void Constructor_WithNegativeAmount_ShouldThrowArgumentException(decimal negativeAmount)
    {
        Assert.Throws<ArgumentException>(() => new Money(negativeAmount, "CZK"));
    }

    [Theory]
    [InlineData(10.123)]
    [InlineData(0.001)]
    public void Constructor_WithMoreThanTwoDecimalPlaces_ShouldThrowArgumentException(decimal invalidPrecisionAmount)
    {
        Assert.Throws<ArgumentException>(() => new Money(invalidPrecisionAmount, "CZK"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_WithNullOrWhiteSpaceCurrency_ShouldThrowArgumentException(string? invalidCurrency)
    {
        Assert.Throws<ArgumentException>(() => new Money(100m, invalidCurrency!));
    }

    [Theory]
    [InlineData("CZ")]
    [InlineData("CZKK")]
    [InlineData("  US  ")]
    public void Constructor_WithCurrencyLengthNotEqualToThree_ShouldThrowArgumentException(string invalidLengthCurrency)
    {
        Assert.Throws<ArgumentException>(() => new Money(100m, invalidLengthCurrency));
    }

    [Fact]
    public void Equality_WithSameAmountAndNormalizedCurrency_ShouldBeEqual()
    {
        var money1 = new Money(500m, "czk");
        var money2 = new Money(500.00m, "  CZK ");

        Assert.Equal(money1, money2);
    }

    [Fact]
    public void Equality_WithDifferentCurrencies_ShouldNotBeEqual()
    {
        var money1 = new Money(500m, "CZK");
        var money2 = new Money(500m, "EUR");

        Assert.NotEqual(money1, money2);
    }

    [Fact]
    public void Equality_WithDifferentAmounts_ShouldNotBeEqual()
    {
        var money1 = new Money(500m, "CZK");
        var money2 = new Money(600m, "CZK");

        Assert.NotEqual(money1, money2);
    }
}