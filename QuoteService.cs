using System.Text.Json;

namespace Consumo_API_CSharp;

/// <summary>
/// Serviço responsável por fazer os pedidos HTTP GET à API DummyJSON.
/// </summary>
public class QuoteService
{
    private const string BaseUrl = "https://dummyjson.com/quotes";

    private static readonly HttpClient Client = new()
    {
        Timeout = TimeSpan.FromSeconds(10)
    };

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Obtém uma lista de citações.
    /// </summary>
    public async Task<List<Quote>> GetQuotesAsync(int limit)
    {
        string json = await GetJsonAsync($"{BaseUrl}?limit={limit}");

        QuotesResponse? resposta = JsonSerializer.Deserialize<QuotesResponse>(json, JsonOptions);

        return resposta?.Quotes ?? new List<Quote>();
    }

    /// <summary>
    /// Obtém uma citação aleatória.
    /// </summary>
    public async Task<Quote?> GetRandomQuoteAsync()
    {
        string json = await GetJsonAsync($"{BaseUrl}/random");

        return JsonSerializer.Deserialize<Quote>(json, JsonOptions);
    }

    private static async Task<string> GetJsonAsync(string url)
    {
        // Pedido HTTP do tipo GET
        using HttpResponseMessage resposta = await Client.GetAsync(url);

        // Lança excepção se o código HTTP não for de sucesso (200-299)
        resposta.EnsureSuccessStatusCode();

        return await resposta.Content.ReadAsStringAsync();
    }
}