using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypt8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoinController: ControllerBase
{
    private readonly ICoinMarketCapService _coinService;
    private readonly ICoinRepository _coinRepository;

    public CoinController(ICoinMarketCapService coinService, ICoinRepository coinRepository)
    {
        _coinService = coinService;
        _coinRepository = coinRepository;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCoins(CancellationToken cancellationToken)
    {
        var coins = await _coinRepository.GetAllAsync(cancellationToken);

        if (coins == null || !coins.Any())
        {
            Console.WriteLine("No coins found in database.");
            return NotFound("No coins found in the database.");
        }

        return Ok(coins);
    }

    [HttpGet("compare")]
    public async Task<IActionResult> CompareMarketCapOfTwoCoins(string coin1, string coin2, CancellationToken cancellationToken)
    {
        var firstCoin = await _coinService.GetByNameOrSymbolAsync(coin1, cancellationToken);
        var secondCoin = await _coinService.GetByNameOrSymbolAsync(coin2, cancellationToken);

        var aWithMarketCapB = firstCoin.MarketCap / secondCoin.TotalSupply;

        return Ok(new {
            ACoinSymbol = firstCoin.Symbol,
            BCoinSymbol = secondCoin.Symbol,
            ACoinWithMarketCapOfBCoin = aWithMarketCapB.ToString("F2"),
            NumberOfA_X = (aWithMarketCapB / secondCoin.Price).ToString("F2"),
            NumberOfB_X = (secondCoin.Price / aWithMarketCapB).ToString("F2")
        });
    }
}