using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public class CoinMarketCapResponse
{
    public Status Status { get; set; }
    public List<CoinData> Data { get; set; }
}

public class Status
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public int CreditCount { get; set; }
    public DateTime Timestamp { get; set; }
}

public class CoinData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Slug { get; set; }
    public int NumMarketPairs { get; set; }
    public DateTime DateAdded { get; set; }
    public List<string> Tags { get; set; }
    public decimal? MaxSupply { get; set; }
    public decimal CirculatingSupply { get; set; }
    public decimal TotalSupply { get; set; }
    public bool InfiniteSupply { get; set; }
    public int CmcRank { get; set; }
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
    public decimal Volume24h { get; set; }
}