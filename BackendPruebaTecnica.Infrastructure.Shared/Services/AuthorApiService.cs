using System.Net.Http.Json;
using BackendPruebaTecnica.Application.DTOs;
using BackendPruebaTecnica.Application.DTOs.Author;
using BackendPruebaTecnica.Application.Interfaces.API;
using Newtonsoft.Json;

namespace BackendPruebaTecnica.Infrastructure.Shared.Services;

public class AuthorApiService : IAuthorApiService
{
    private readonly HttpClient _httpClient;

    public AuthorApiService(HttpClient  httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<AuthorDTOs> CreateAuthorAsync(AuthorDTOs createAuthor,CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("https://fakerestapi.azurewebsites.net/api/v1/Authors", createAuthor, cancellationToken: cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<AuthorDTOs>(cancellationToken: cancellationToken);
        return responseData!;
    }

    public async Task<AuthorDTOs> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetStringAsync($"https://fakerestapi.azurewebsites.net/api/v1/Authors/{id}", cancellationToken);
        return JsonConvert.DeserializeObject<AuthorDTOs>(response)!;
    }

    public async Task<IEnumerable<AuthorDTOs>> GetAuthorsAsync(CancellationToken cancellationToken) => 
        (await _httpClient.GetFromJsonAsync<IEnumerable<AuthorDTOs>>("https://fakerestapi.azurewebsites.net/api/v1/Authors", cancellationToken))!;
    
    public async Task<AuthorDTOs> UpdateAuthorAsync(int id, AuthorDTOs author, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://fakerestapi.azurewebsites.net/api/v1/Authors/{id}", author, cancellationToken: cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<AuthorDTOs>(cancellationToken: cancellationToken);
        return responseData!;
    }

    public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"https://fakerestapi.azurewebsites.net/api/v1/Authors/{id}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
    public async Task<IEnumerable<AuthorDTOs>> GetAuthorsByBookIdAsync(int idBook, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetStringAsync("https://fakerestapi.azurewebsites.net/api/v1/Authors",  cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(response))
            return Enumerable.Empty<AuthorDTOs>();

        var authors = JsonConvert.DeserializeObject<IEnumerable<AuthorDTOs>>(response) ?? Enumerable.Empty<AuthorDTOs>();

        return authors.Where(author => author.IdBook == idBook);
    }
}