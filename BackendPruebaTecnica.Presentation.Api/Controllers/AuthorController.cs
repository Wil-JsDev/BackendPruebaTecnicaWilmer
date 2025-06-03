using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace BackendPruebaTecnica.Presentation.Api.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;    
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDTOs>>> GetPagedAuthors(CancellationToken cancellationToken,
        int pageNumber = 1 ,int pageSize = 15)
    {
        var result = await _authorService.GetPagedAuthors(pageSize, pageNumber, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return BadRequest(result.Error);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTOs>> GetByIdAuthor([FromRoute] int id, CancellationToken  cancellationToken)
    {
        var result = await _authorService.GetAuthorByIdAsync(id, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return NotFound(result.Error);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDTOs>> CreateAuthor(AuthorDTOs author,
        CancellationToken cancellationToken)
    {
        var result = await _authorService.CreateAuthorAsync(author, cancellationToken);
        
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AuthorDTOs>> UpdateAuthor(int id,UpdateAuthorDTOs author,
        CancellationToken cancellationToken)
    {
        var result = await _authorService.UpdateAuthorAsync(id, author, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AuthorDTOs>> DeleteAuthor([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var result = await _authorService.DeleteAuthorAsync(id, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return BadRequest(result.Error);
    }

    [HttpGet("/books/{idBook}")]
    public async Task<IActionResult> GetAuthorsByBookId(int idBook, CancellationToken cancellationToken)
    {
        var result = await _authorService.GetAuthorByBookdIdAsync(idBook, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        
        return NotFound(result.Error);
    }
    
}