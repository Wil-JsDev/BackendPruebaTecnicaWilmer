using BackendPruebaTecnica.Application.DTOs.Books;
using BackendPruebaTecnica.Application.Interfaces.API;
using BackendPruebaTecnica.Application.Interfaces.Service;
using BackendPruebaTecnica.Application.Utils;

namespace BackendPruebaTecnica.Application.Services;

public class BookService : IBookService
{
    
    private readonly IBooksApiService _booksApiService;

    public BookService(IBooksApiService  booksApiService)
    {
        _booksApiService = booksApiService;
    }
    
    public async Task<ResultT<BookDTOs>> CreateBookAsync(BookDTOs book, CancellationToken cancellationToken)
    {
        if (book == null)
        {
            return ResultT<BookDTOs>.Failure(
                Error.Failure("400", "Book data cannot be null."));
        }

        var createdAuthor = await _booksApiService.CreateBookAsync(book, cancellationToken);
    
        return ResultT<BookDTOs>.Success(createdAuthor);
    }

    public async Task<ResultT<BookDTOs>> UpdateBookAsync(int id, UpdateBookDTOs book, CancellationToken cancellationToken)
    {
        var existingBook = await _booksApiService.GetBookByIdAsync(id, cancellationToken);

        if (existingBook == null)
        {
            return ResultT<BookDTOs>.Failure(
                Error.NotFound("404", $"No book found with ID {id} to update."));
        }

        existingBook = existingBook with
        {
            Title = book.Title,
            Description = book.Description,
            Excerpt = book.Excerpt,
            PublishDate = book.PublishDate
        };

        var updateBook = await _booksApiService.UpdateBookAsync(id, existingBook, cancellationToken);

        return ResultT<BookDTOs>.Success(updateBook);
    }

    public async Task<ResultT<PagedResult<BookDTOs>>> GetPagedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1 || pageSize < 1)
        {
            return ResultT<PagedResult<BookDTOs>>.Failure(Error.Failure("400", "pageNumber and pageSize must be greater than 0"));
        }

        var allBooks = await _booksApiService.GetAllBooksAsync(cancellationToken);

        if (!allBooks.Any())
        {
            return ResultT<PagedResult<BookDTOs>>.Failure(Error.Failure("404", "No books were found in the system."));
        }

        var totalRecords = allBooks.Count();

        var paginatedBooks = allBooks
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new BookDTOs(
                Id: x.Id,
                Title: x.Title,
                Description: x.Description,
                Excerpt: x.Excerpt,
                PublishDate: x.PublishDate
            ));

        var response = new PagedResult<BookDTOs>(
            TotalRecords: totalRecords,
            PageNumber: pageNumber,
            PageSize: pageSize,
            Items: paginatedBooks
        );

        return ResultT<PagedResult<BookDTOs>>.Success(response);
    }


    public async Task<ResultT<BookDTOs>> GetBookByIdAsync(int id, CancellationToken cancellationToken)
    {
        var  book = await _booksApiService.GetBookByIdAsync(id, cancellationToken);
        if (book == null)
        {
            return ResultT<BookDTOs>.Failure(Error.NotFound("404","Book not found."));
        }

        BookDTOs bookDtOs = new
        (
            Id: book.Id,
            Title: book.Title,
            Description: book.Description,
            Excerpt: book.Excerpt,
            PublishDate:  book.PublishDate
        );
        
        return ResultT<BookDTOs>.Success(bookDtOs);
    }

    public async Task<ResultT<string>> DeletedBookAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _booksApiService.GetBookByIdAsync(id, cancellationToken);
        if (book == null)
        {
            return ResultT<string>.Failure(Error.NotFound("404","Book not found."));
        }
        
        var bookDeleted = await _booksApiService.DeleteBookAsync(id, cancellationToken);
        
        if (!bookDeleted)
        {
            return ResultT<string>.Failure(Error.Failure("400", $"Failed to delete book with ID {id}."));
        }

        return ResultT<string>.Success($"Book with ID {id} was successfully deleted.");
    }
}