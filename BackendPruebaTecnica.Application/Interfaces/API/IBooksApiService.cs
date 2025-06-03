using BackendPruebaTecnica.Application.DTOs.Books;

namespace BackendPruebaTecnica.Application.Interfaces.API;

public interface IBooksApiService
{
    Task<BookDTOs> CreateBookAsync(BookDTOs book, CancellationToken cancellationToken);

    Task<BookDTOs> GetBookByIdAsync(int id, CancellationToken cancellationToken);

    Task<BookDTOs> UpdateBookAsync(int id, BookDTOs book, CancellationToken cancellationToken);

    Task<IEnumerable<BookDTOs>> GetAllBooksAsync(CancellationToken cancellationToken);

    Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
}