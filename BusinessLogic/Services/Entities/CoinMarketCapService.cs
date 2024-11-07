using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;


namespace BusinessLogic.Services.Entities;

public class CoinMarketCapService : ICoinMarketCapService
{
    private readonly ICoinRepository _coinRepository;
    private readonly HttpClient _httpClient;

    public CoinMarketCapService(ICoinRepository coinRepository)
    {
        _coinRepository = coinRepository;
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://pro-api.coinmarketcap.com")
        };
        _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "9ca43cc8-c5fd-4049-9bb3-3df4de6d2e74");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public async Task CreateAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Attempting to fetch data from CoinMarketCap API...");

        var response = await _httpClient.GetAsync("/v1/cryptocurrency/listings/latest?limit=100", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to fetch data: {response.ReasonPhrase}");
            throw new Exception($"Failed to fetch data: {response.ReasonPhrase}");
        }

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Raw API Response");

        var result = JsonSerializer.Deserialize<CoinMarketCapResponse>(content);
        if (result?.Data != null)
        {
            foreach (var coinData in result.Data)
            {
                //Console.WriteLine(string.Format("Name: {0}\nMCAP: {1}\nTOTAL SUPPLY: {2}\nPRICE: {3}\n\n\n", coinData.Name, coinData.Quote.USD.MarketCap, coinData.TotalSupply, coinData.Quote.USD.Price));
                var coin = new Coin
                {
                    Name = coinData.Name,
                    Symbol = coinData.Symbol,
                    MarketCap = coinData.Quote.USD.MarketCap,
                    TotalSupply = coinData.CirculatingSupply,
                    Price = coinData.Quote.USD.Price
                };
                await _coinRepository.CreateAsync(coin);
                Console.WriteLine($"Saved coin: {coin.Name}");
            }
            Console.WriteLine("All coins saved successfully.");
        }
        else
        {
            Console.WriteLine("No data received from CoinMarketCap.");
        }
    }

    public async Task<Coin> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var coin = await _coinRepository.GetByNameAsync(name, cancellationToken);

        if (coin == null)
        {
            return null;
        }

        return coin;
    }

    public async Task<Coin> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var coin = await _coinRepository.GetBySymbolAsync(symbol, cancellationToken);

        if (coin == null)
        {
            return null;
        }

        return coin;
    }
}
