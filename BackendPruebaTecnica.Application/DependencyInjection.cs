using BackendPruebaTecnica.Application.Interfaces.Service;
using BackendPruebaTecnica.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BackendPruebaTecnica.Application;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
    }
    
}