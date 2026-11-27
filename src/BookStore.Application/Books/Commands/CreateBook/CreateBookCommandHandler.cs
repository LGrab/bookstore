using BookStore.Application.Abstractions.Repositories;
using BookStore.Domain.Entities;
using BookStore.Domain.ValueObjects;
using MediatR;

namespace BookStore.Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var isbn = new Isbn(request.Isbn);

        if (await _bookRepository.ExistsByIsbnAsync(isbn, cancellationToken))
        {
            throw new InvalidOperationException(
                "A book with this ISBN already exists.");
        }

        var title = new Title(request.Title);
        var author = new Author(request.AuthorFirstName, request.AuthorLastName);
        var stockQuantity = request.StockQuantity;
        var price = new Money(request.Price, request.Currency);

        var book = Book.Create(title, author, isbn, stockQuantity, price);

        await _bookRepository.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}