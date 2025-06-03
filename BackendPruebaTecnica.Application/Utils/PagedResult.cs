namespace BackendPruebaTecnica.Application.Utils;

public record PagedResult<T>
(
    int TotalRecords,
    int PageNumber,
    int PageSize,
    IEnumerable<T> Items
);