using FluentValidation;

namespace BookStore.Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title cannot be longer than 200 characters.");

        RuleFor(x => x.AuthorFirstName)
            .NotEmpty()
            .WithMessage("Author first name is required.")
            .MaximumLength(100);

        RuleFor(x => x.AuthorLastName)
            .NotEmpty()
            .WithMessage("Author last name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .WithMessage("Isbn  is required.")
            .Length(10)
            .WithMessage("Isbn10 has to be 10 characters long.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price cannot be negative.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3)
            .WithMessage("Currency must contain exactly 3 characters.");
    }
}