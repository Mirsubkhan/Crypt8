using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace BusinessLogic.Services.Entities
{
    public class CoinMarketCapService
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

        public async Task FetchAndSaveTop100CoinsAsync()
        {
            var response = await _httpClient.GetAsync("/v1/cryptocurrency/listings/latest?=limit=100");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CoinMarketCapResponse>(content);

            if (result?.Data != null)
            {
                foreach (var coinData in result.Data)
                {
                    var coin = new Coin
                    {
                        Name = coinData.Name,
                        MarketCap = coinData.Quote.USD.MarketCap,
                        TotalSupply = coinData.TotalSupply,
                        Price = coinData.Quote.USD.Price
                    };
                    await _coinRepository.CreateAsync(coin);
                }
            }
        }
    }

    public class CoinMarketCapResponse
    {
        public List<CoinData> Data { get; set; }
    }

    public class CoinData
    {
        public string Name { get; set; }
        public decimal TotalSupply { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public USD USD { get; set; }
    }

    public class USD
    {
        public decimal Price { get; set; }
        public decimal MarketCap { get; set; }
    }
}
