using BookStore.Domain.Entities;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.Abstractions.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIsbnAsync(Isbn isbn, CancellationToken cancellationToken = default);
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
}