using BackendPruebaTecnica.Application.Interfaces.API;
using BackendPruebaTecnica.Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackendPruebaTecnica.Infrastructure.Shared;

public static class DependencyInjection
{
    public static void AddSharedLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthorApiService, AuthorApiService>();
        services.AddScoped<IBooksApiService, BooksApiService>();
    }
}