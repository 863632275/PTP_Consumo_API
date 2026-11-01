using System.Text.Json.Serialization;

namespace Consumo_API_CSharp;

/// <summary>
/// Representa uma citação devolvida pela API DummyJSON.
/// </summary>
public class Quote
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("quote")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;
}

/// <summary>
/// Representa a resposta da lista de citações (https://dummyjson.com/quotes).
/// </summary>
public class QuotesResponse
{
    [JsonPropertyName("quotes")]
    public List<Quote> Quotes { get; set; } = new();

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }
}