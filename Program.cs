using System.Text;
using System.Text.Json;
using Consumo_API_CSharp;

Console.OutputEncoding = Encoding.UTF8;

var servico = new QuoteService();
bool continuar = true;

while (continuar)
{
    Console.WriteLine();
    Console.WriteLine("=== Consumo de API: Citações (DummyJSON) ===");
    Console.WriteLine("1 - Mostrar uma citação aleatória");
    Console.WriteLine("2 - Mostrar 5 citações");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolhe uma opção: ");

    string? opcao = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (opcao)
        {
            case "1":
                Quote? citacao = await servico.GetRandomQuoteAsync();

                if (citacao is null)
                {
                    Console.WriteLine("A API não devolveu nenhuma citação.");
                }
                else
                {
                    MostrarCitacao(citacao);
                }
                break;

            case "2":
                List<Quote> citacoes = await servico.GetQuotesAsync(5);

                if (citacoes.Count == 0)
                {
                    Console.WriteLine("A API não devolveu nenhuma citação.");
                }
                else
                {
                    foreach (Quote q in citacoes)
                    {
                        MostrarCitacao(q);
                    }
                }
                break;

            case "0":
                continuar = false;
                Console.WriteLine("Até breve!");
                break;

            default:
                Console.WriteLine("Opção inválida. Tenta novamente.");
                break;
        }
    }
    catch (HttpRequestException ex)
    {
        // Falha de ligação (sem internet, DNS, servidor em baixo ou código HTTP de erro)
        Console.WriteLine("Erro de ligação à API. Verifica a tua internet e tenta de novo.");
        Console.WriteLine($"Detalhe: {ex.Message}");
    }
    catch (TaskCanceledException)
    {
        // O pedido demorou mais do que o tempo limite (10 segundos)
        Console.WriteLine("O pedido demorou demasiado tempo (timeout). Tenta novamente.");
    }
    catch (JsonException ex)
    {
        // A resposta recebida não estava no formato JSON esperado
        Console.WriteLine("Erro ao processar os dados JSON recebidos.");
        Console.WriteLine($"Detalhe: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocorreu um erro inesperado.");
        Console.WriteLine($"Detalhe: {ex.Message}");
    }
}

static void MostrarCitacao(Quote q)
{
    Console.WriteLine($"[{q.Id}] \"{q.Text}\"");
    Console.WriteLine($"    - {q.Author}");
    Console.WriteLine();
}