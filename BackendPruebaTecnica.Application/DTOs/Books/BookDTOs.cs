namespace BackendPruebaTecnica.Application.DTOs.Books;

public record BookDTOs
(
    int Id,
    string Title,
    string Description,
    string Excerpt,
    DateTime PublishDate
);
