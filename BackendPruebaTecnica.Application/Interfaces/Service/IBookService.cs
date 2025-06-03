using BackendPruebaTecnica.Application.DTOs.Books;
using BackendPruebaTecnica.Application.Utils;

namespace BackendPruebaTecnica.Application.Interfaces.Service;

public interface IBookService
{
    Task<ResultT<BookDTOs>> CreateBookAsync(BookDTOs book, CancellationToken cancellationToken);
    
    Task<ResultT<BookDTOs>> UpdateBookAsync(int id, UpdateBookDTOs book, CancellationToken cancellationToken);

    Task<ResultT<PagedResult<BookDTOs>>> GetPagedBooksAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<ResultT<BookDTOs>> GetBookByIdAsync(int id, CancellationToken cancellationToken);

    Task<ResultT<string>> DeletedBookAsync(int id, CancellationToken cancellationToken);
}