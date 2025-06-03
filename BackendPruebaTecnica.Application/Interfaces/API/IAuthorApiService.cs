using BackendPruebaTecnica.Application.DTOs.Author;

namespace BackendPruebaTecnica.Application.Interfaces.API;

public interface IAuthorApiService
{
    Task<AuthorDTOs> CreateAuthorAsync(AuthorDTOs createAuthor, CancellationToken cancellationToken);

    Task<AuthorDTOs> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<AuthorDTOs>> GetAuthorsAsync(CancellationToken cancellationToken);

    Task<AuthorDTOs> UpdateAuthorAsync(int id, AuthorDTOs author, CancellationToken cancellationToken);

    Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken);

}
