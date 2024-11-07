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
            return NotFound("No coins found in the database.");
        }
        return Ok(coins);
    }

    [HttpGet("compare")]
    public async Task<IActionResult> CompareMarketCapOfTwoCoins(string coin1, string coin2, CancellationToken cancellationToken)
    {
        try
        {
            var firstCoin = await _coinService.GetByNameAsync(coin1, cancellationToken);
            var secondCoin = await _coinService.GetByNameAsync(coin2, cancellationToken);

            if (firstCoin == null || secondCoin == null)
            {
                firstCoin = await _coinService.GetBySymbolAsync(coin1, cancellationToken);
                secondCoin = await _coinService.GetBySymbolAsync(coin2, cancellationToken);

                if (firstCoin == null || secondCoin == null)
                {
                    return NotFound("Coin Not Found");   
                }
            }

            var aWithMarketCapB = firstCoin.MarketCap / secondCoin.TotalSupply;

            return Ok(new {
                ASymbol = firstCoin.Symbol,
                BSymbol = secondCoin.Symbol,
                AWithMarketCapB = aWithMarketCapB.ToString("F2"),
                BX = (aWithMarketCapB / secondCoin.Price).ToString("F2"),
                AX = (secondCoin.Price / aWithMarketCapB).ToString("F2")
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}