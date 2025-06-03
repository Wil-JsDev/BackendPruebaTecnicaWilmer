namespace BackendPruebaTecnica.Application.DTOs.Books;

public record UpdateBookDTOs
(
    string Title,
    string Description,
    string Excerpt,
    DateTime PublishDate
);