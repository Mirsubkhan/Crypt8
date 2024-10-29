using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

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
