using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.Interfaces.Service;
using BackendPruebaTecnica.Application.Utils;
using BackendPruebaTecnica.Presentation.Api.Controllers;
using Moq;

namespace BackendPruebaTecnica.Tests;

public class AuthorControllerTests
{
    
    private readonly Mock<IAuthorService> _mock = new();
    
    [Fact]
    public void CreateAuthorController()
    {
        
        // Arrange
        
        var author = new AuthorDTOs(
            Id: 1,
            IdBook: 101,
            FirstName: "Gabriel",
            LastName: "García Márquez"
        );

        AuthorDTOs AuthorDTos = new
        (
            Id: author.Id,
            IdBook: author.IdBook,
            FirstName: author.FirstName,
            LastName: author.LastName
        );
        
        var expectedResult = ResultT<AuthorDTOs>.Success(AuthorDTos);
        
        _mock.Setup( m => m.CreateAuthorAsync(author, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var authorController = new AuthorController(_mock.Object);
        
        // Act
        var result = authorController.CreateAuthor(AuthorDTos, CancellationToken.None);
        
        // Assert

        Assert.NotNull(result);
        
    }

    [Fact]
    public void GetAllAuthorController()
    {
        // Arrange

        int page = 1;
        int pageSize = 10;

        PagedResult<AuthorDTOs> pagedResult = new(
            TotalRecords: 1,
            PageNumber: page,
            PageSize: pageSize,
            Items: new List<AuthorDTOs>()
        );
        
        var expectedResult = ResultT<PagedResult<AuthorDTOs>>.Success(pagedResult);
        
        _mock.Setup(m => m.GetPagedAuthors(page, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);
        
        var authorController = new AuthorController(_mock.Object);
        
        // Act

        var result = authorController.GetPagedAuthors(cancellationToken:  CancellationToken.None, page, pageSize);

        // Assert
        
        Assert.NotNull(result);
    }

    [Fact]
    public void GetByIdAuthorController()
    {
        // Arrange
        var authorId = 1;
        var expectedAuthor = new AuthorDTOs(
            Id: 1,
            IdBook: 101,
            FirstName: "Gabriel",
            LastName: "García Márquez"
        );

        var expectedResult = ResultT<AuthorDTOs>.Success(expectedAuthor);

        _mock.Setup(m => m.GetAuthorByIdAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var authorController = new AuthorController(_mock.Object);

        // Act
        var result = authorController.GetByIdAuthor(authorId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void UpdateAuthorController()
    {
        // Arrange
        var authorId = 1;
    
        var updateDto = new UpdateAuthorDTOs(
            IdBook: 101,
            FirstName: "Gabriel",
            LastName: "García Márquez"
        );

        var expectedAuthor = new AuthorDTOs(
            Id: authorId,
            IdBook: updateDto.IdBook,
            FirstName: updateDto.FirstName,
            LastName: updateDto.LastName
        );

        var expectedResult = ResultT<AuthorDTOs>.Success(expectedAuthor);

        _mock.Setup(m => m.UpdateAuthorAsync(authorId, updateDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new AuthorController(_mock.Object);

        // Act
        var result =  controller.UpdateAuthor(authorId, updateDto, CancellationToken.None);

        // Assert
        
        Assert.NotNull(result);
        
    }

    [Fact]
    public void DeleteAuthorController()
    {
        // Arrange
        var authorId = 1;
        string message = $"Author with ID {authorId} was successfully deleted.";

        var expectedResult = ResultT<string>.Success(message);

        _mock.Setup(m => m.DeleteAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new AuthorController(_mock.Object);

        // Act
        
        var result = controller.DeleteAuthor(authorId, CancellationToken.None);

        // Assert
    
        Assert.NotNull(result);  
    }
    
}