using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.Utils;

namespace BackendPruebaTecnica.Application.Interfaces.Service;

public interface IAuthorService
{
    Task<ResultT<PagedResult<AuthorDTOs>>> GetPagedAuthors(int page, int pageSize, CancellationToken cancellationToken);
    
    Task<ResultT<AuthorDTOs>> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
    
    Task<ResultT<AuthorDTOs>> CreateAuthorAsync(AuthorDTOs author, CancellationToken cancellationToken);
    
    Task<ResultT<AuthorDTOs>> UpdateAuthorAsync(int id, UpdateAuthorDTOs author, CancellationToken cancellationToken);
    
    Task<ResultT<string>> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
    
    Task<ResultT<IEnumerable<AuthorDTOs>>> GetAuthorByBookdIdAsync(int idBook, CancellationToken cancellationToken);
}