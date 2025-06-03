using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.Interfaces.API;
using BackendPruebaTecnica.Application.Interfaces.Service;
using BackendPruebaTecnica.Application.Utils;

namespace BackendPruebaTecnica.Application.Services;

public class AuthorService : IAuthorService
{
    private IAuthorApiService  _authorApiService;
    private IBooksApiService  _booksApiService;
    public AuthorService(IAuthorApiService authorApiService,  IBooksApiService booksApiService)
    {
        _authorApiService = authorApiService;
        _booksApiService = booksApiService;
    }
    
    public async Task<ResultT<PagedResult<AuthorDTOs>>> GetPagedAuthors(int pageNumber, int pageSize,CancellationToken cancellationToken)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return ResultT<PagedResult<AuthorDTOs>>.Failure(Error.Failure("400", "pageNumber and pageSize must be greater than 0"));
        }

        var authorList = await _authorApiService.GetAuthorsAsync(cancellationToken);

        if (!authorList.Any())
        {
            return ResultT<PagedResult<AuthorDTOs>>.Failure(Error.Failure("404", "No authors were found in the system."));
        }

        var totalRecords = authorList.Count();
        var paginated = authorList
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new AuthorDTOs(
                Id: x.Id,
                IdBook: x.IdBook,
                FirstName: x.FirstName,
                LastName: x.LastName
            ));

        var response = new PagedResult<AuthorDTOs>(
            TotalRecords: totalRecords,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Items: paginated
        );

        return ResultT<PagedResult<AuthorDTOs>>.Success(response);
    }
    
    public async Task<ResultT<AuthorDTOs>> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _authorApiService.GetAuthorByIdAsync(id, cancellationToken);

        if (author == null)
        {
            return ResultT<AuthorDTOs>.Failure(
                Error.NotFound("404", $"No author found with ID {id}."));
        }

        var authorDto = new AuthorDTOs
        (
            Id: author.Id,
            IdBook: author.IdBook,
            FirstName: author.FirstName,
            LastName: author.LastName
        );

        return ResultT<AuthorDTOs>.Success(authorDto);
    }

    public async Task<ResultT<AuthorDTOs>> CreateAuthorAsync(AuthorDTOs author, CancellationToken cancellationToken)
    {
        if (author == null)
        {
            return ResultT<AuthorDTOs>.Failure(
                Error.Failure("400", "Author data cannot be null."));
        }

        var createdAuthor = await _authorApiService.CreateAuthorAsync(author, cancellationToken);
    
        return ResultT<AuthorDTOs>.Success(createdAuthor);
    }
    
    public async Task<ResultT<AuthorDTOs>> UpdateAuthorAsync(int id, UpdateAuthorDTOs author, CancellationToken cancellationToken)
    {
        var existingAuthor = await _authorApiService.GetAuthorByIdAsync(id, cancellationToken);

        if (existingAuthor == null)
        {
            return ResultT<AuthorDTOs>.Failure(
                Error.NotFound("404", $"No author found with ID {id} to update."));
        }

        existingAuthor = existingAuthor with
        {
            IdBook = author.IdBook,
            FirstName = author.FirstName,
            LastName = author.LastName
        };

        var updatedAuthor = await _authorApiService.UpdateAuthorAsync(id, existingAuthor, cancellationToken);

        return ResultT<AuthorDTOs>.Success(updatedAuthor);
    }

    public async Task<ResultT<string>> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _authorApiService.GetAuthorByIdAsync(id, cancellationToken);
    
        if (author == null)
        {
            return ResultT<string>.Failure(Error.NotFound("404", $"No author found with ID {id}."));
        }

        var deleted = await _authorApiService.DeleteAuthorAsync(author.Id, cancellationToken);
    
        if (!deleted)
        {
            return ResultT<string>.Failure(Error.Failure("400", $"Failed to delete author with ID {id}."));
        }

        return ResultT<string>.Success($"Author with ID {id} was successfully deleted.");
    }

    public async Task<ResultT<IEnumerable<AuthorDTOs>>> GetAuthorByBookdIdAsync(int idBook, CancellationToken cancellationToken)
    {
        var bookExist = await _booksApiService.GetBookByIdAsync(idBook, cancellationToken);
        if (bookExist == null)
        {
            return ResultT<IEnumerable<AuthorDTOs>>.Failure(
                Error.NotFound("404", $"Book with ID {idBook} was not found."));
        }

        var listAuthorDtOsEnumerable = await _authorApiService.GetAuthorsByBookIdAsync(idBook, cancellationToken);
        if (!listAuthorDtOsEnumerable.Any())
        {
            return ResultT<IEnumerable<AuthorDTOs>>.Failure(
                Error.Failure("400", $"No authors were found for the book with ID {idBook}."));
        }

        var authors = listAuthorDtOsEnumerable.Select(x => new AuthorDTOs
        (
            Id: x.Id,
            IdBook: x.IdBook,
            FirstName: x.FirstName,
            LastName: x.LastName
        ));
        
        return ResultT<IEnumerable<AuthorDTOs>>.Success(authors);
    }
}