namespace BackendPruebaTecnica.Application.DTOs.Author;

public record AuthorDTOs
(
    int Id,
    int IdBook,
    string FirstName,
    string LastName
);