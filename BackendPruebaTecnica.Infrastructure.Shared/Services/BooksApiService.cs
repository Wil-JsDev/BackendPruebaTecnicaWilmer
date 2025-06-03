using System.Net.Http.Json;
using System.Text;
using BackendPruebaTecnica.Application.DTOs;
using BackendPruebaTecnica.Application.DTOs.Books;
using BackendPruebaTecnica.Application.Interfaces.API;
using Newtonsoft.Json;

namespace BackendPruebaTecnica.Infrastructure.Shared.Services;

public class BooksApiService : IBooksApiService
{
    private readonly HttpClient _httpClient;

    public BooksApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<BookDTOs> CreateBookAsync(BookDTOs book, CancellationToken cancellationToken)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://fakerestapi.azurewebsites.net/api/v1/Books", content, cancellationToken);
        var responseData = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<BookDTOs>(responseData)!;
    }

    public async Task<BookDTOs> GetBookByIdAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetStringAsync($"https://fakerestapi.azurewebsites.net/api/v1/Books/{id}", cancellationToken);
        return JsonConvert.DeserializeObject<BookDTOs>(response)!;
    }

    public async Task<BookDTOs> UpdateBookAsync(int id, BookDTOs book, CancellationToken cancellationToken)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"https://fakerestapi.azurewebsites.net/api/v1/Books/{id}", content, cancellationToken);
        var responseData = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonConvert.DeserializeObject<BookDTOs>(responseData)!;
    }

    public async Task<IEnumerable<BookDTOs>> GetAllBooksAsync(CancellationToken cancellationToken) => 
        (await _httpClient.GetFromJsonAsync<IEnumerable<BookDTOs>>("https://fakerestapi.azurewebsites.net/api/v1/Books", cancellationToken))!;
    
    public async Task<bool> DeleteBookAsync(int id,  CancellationToken cancellationToken)
    {
        var response = await _httpClient.DeleteAsync($"https://fakerestapi.azurewebsites.net/api/v1/Books/{id}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}