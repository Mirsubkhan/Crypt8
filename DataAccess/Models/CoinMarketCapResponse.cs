using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models;

public class CoinMarketCapResponse
{
    [JsonPropertyName("data")]
    public List<CoinData> Data { get; set; }
}

public class CoinData
{
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }
    [JsonPropertyName("max_supply")]
    public decimal? MaxSupply { get; set; }
    [JsonPropertyName("circulating_supply")]
    public decimal CirculatingSupply { get; set; }
    [JsonPropertyName("total_supply")]
    public decimal TotalSupply { get; set; }
    [JsonPropertyName("cmc_rank")]
    public int CmcRank { get; set; }

    [JsonPropertyName("quote")]
    public Quote Quote { get; set; }
}

public class Quote
{
    [JsonPropertyName("USD")]
    public USD USD { get; set; }
}

public class USD
{
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("market_cap")]
    public decimal MarketCap { get; set; }
    [JsonPropertyName("volume_24h")]
    public decimal Volume24h { get; set; }
}