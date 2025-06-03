using BackendPruebaTecnica.Application.DTOs.Books;
using BackendPruebaTecnica.Application.Interfaces.Service;
using BackendPruebaTecnica.Application.Utils;
using BackendPruebaTecnica.Presentation.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BackendPruebaTecnica.Tests;

public class BooksControllerTests
{

    private readonly Mock<IBookService> _mock = new();
    
    [Fact]
    public void CreateBookController()
    {
        // Arrange
        var newBook = new BookDTOs(
            Id: 1,
            Title: "One Hundred Years of Solitude",
            Description: "A masterpiece of Latin American literature.",
            Excerpt: "Many years later, as he faced the firing squad...",
            PublishDate: new DateTime(1967, 5, 30)
        );

        var expectedResult = ResultT<BookDTOs>.Success(newBook);

        _mock.Setup(m => m.CreateBookAsync(newBook, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new BookController(_mock.Object);

        // Act
        var result = controller.CreateBook(newBook, CancellationToken.None).Result;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetBookByIdController()
    {
        // Arrange
        var bookId = 1;

        var expectedBook = new BookDTOs(
            Id: 1,
            Title: "One Hundred Years of Solitude",
            Description: "A masterpiece of Latin American literature.",
            Excerpt: "Many years later, as he faced the firing squad...",
            PublishDate: new DateTime(1967, 5, 30)
        );


        var expectedResult = ResultT<BookDTOs>.Success(expectedBook);

        _mock.Setup(m => m.GetBookByIdAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new BookController(_mock.Object);

        // Act
        var result = controller.GetBookById(bookId, CancellationToken.None).Result;

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public void GetPagedBooksController()
    {
        // Arrange
        int page = 1;
        int pageSize = 10;

        PagedResult<BookDTOs> pagedResult = new(
            TotalRecords: 1,
            PageNumber: page,
            PageSize: pageSize,
            Items: new List<BookDTOs>()
        );

        var expectedResult = ResultT<PagedResult<BookDTOs>>.Success(pagedResult);

        _mock.Setup(m => m.GetPagedBooksAsync(1, 15, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new BookController(_mock.Object);

        // Act
        var result = controller.GetPagedBooks(CancellationToken.None, 1, 15).Result;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void UpdateBookController()
    {
        // Arrange
        var bookId = 1;
        var updateBookDto = new UpdateBookDTOs
        (
            Title: "One Hundred Years of Solitude",
            Description: "A masterpiece of Latin American literature.",
            Excerpt: "Many years later, as he faced the firing squad...",
            PublishDate: new DateTime(1967, 5, 30)
        );

        var updatedBook = new BookDTOs(
            Id: bookId,
            Title: updateBookDto.Title,
            Description: updateBookDto.Description,
            Excerpt: updateBookDto.Excerpt,
            PublishDate: updateBookDto.PublishDate
        );

        var expectedResult = ResultT<BookDTOs>.Success(updatedBook);

        _mock.Setup(m => m.UpdateBookAsync(bookId, updateBookDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new BookController(_mock.Object);

        // Act
        var result = controller.UpdateBook(bookId, updateBookDto, CancellationToken.None).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBook = Assert.IsType<BookDTOs>(okResult.Value);

        Assert.Equal(bookId, returnedBook.Id);
        Assert.Equal(updateBookDto.Title, returnedBook.Title);
    }

    [Fact]
    public void DeleteBookController()
    {
        // Arrange
        var bookId = 1;
        var successMessage = "Book successfully deleted";

        var expectedResult = ResultT<string>.Success(successMessage);

        _mock.Setup(m => m.DeletedBookAsync(bookId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var controller = new BookController(_mock.Object);

        // Act
        var result = controller.DeleteBook(bookId, CancellationToken.None).Result;

        // Assert
       Assert.NotNull(result);
    }

    
}