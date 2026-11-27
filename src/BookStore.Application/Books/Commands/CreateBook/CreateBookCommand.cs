using MediatR;

namespace BookStore.Application.Books.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    string AuthorFirstName,
    string AuthorLastName,
    string Isbn,
    int StockQuantity,
    decimal Price,
    string Currency
) : IRequest<Guid>;