using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.DTOs.Books;
using BackendPruebaTecnica.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendPruebaTecnica.Presentation.Api.Controllers;


[Route("api/books")]
[ApiController]
public class BookController : Controller
{
   private readonly IBookService _bookService;

   public BookController(IBookService bookService)
   {
      _bookService = bookService;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<BookDTOs>>> GetPagedBooks(CancellationToken cancellationToken,
      int pageNumber = 1,int pageSize = 15)
   {
      var result = await _bookService.GetPagedBooksAsync(pageNumber, pageSize, cancellationToken);
      if (result.IsSuccess)
         return Ok(result.Value);
      
      return BadRequest(result.Error);
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<BookDTOs>> GetBookById(int id, CancellationToken cancellationToken)
   {
      var result = await _bookService.GetBookByIdAsync(id,cancellationToken);
      if (result.IsSuccess)
         return Ok(result.Value);
      
      return NotFound(result.Error);
   }

   [HttpPost]
   public async Task<ActionResult<BookDTOs>> CreateBook(BookDTOs book, CancellationToken cancellationToken)
   {
      var result = await _bookService.CreateBookAsync(book, cancellationToken);
      if (result.IsSuccess)
         return Ok(result.Value);
      
      return BadRequest(result.Error);
   }
   
   [HttpPut("{id}")]
   public async Task<ActionResult<BookDTOs>> UpdateBook([FromRoute] int id, UpdateBookDTOs book,
      CancellationToken cancellationToken)
   {
         var result = await _bookService.UpdateBookAsync(id, book, cancellationToken);
         if (result.IsSuccess)
            return Ok(result.Value);
         
         return NotFound(result.Error);
   }
   
   [HttpDelete("{id}")]
   public async Task<ActionResult<string>> DeleteBook([FromRoute] int id, CancellationToken cancellationToken)
   {
      var result = await _bookService.DeletedBookAsync(id,  cancellationToken);
      if (result.IsSuccess)
         return Ok(result.Value);
      
      return BadRequest(result.Error);
   }
   
}