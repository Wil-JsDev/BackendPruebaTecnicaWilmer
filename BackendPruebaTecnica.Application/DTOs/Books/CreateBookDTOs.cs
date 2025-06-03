namespace BackendPruebaTecnica.Application.DTOs.Books;

public record CreateBookDTOs
(
    string Title,
    string Description,
    string Excerpt,
    DateTime PublishDate
);