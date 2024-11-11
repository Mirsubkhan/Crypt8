using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Crypt8.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CoinController: ControllerBase
{
    private readonly ICoinMarketCapService _coinService;
    private readonly ICoinRepository _coinRepository;
    private readonly ICompareHistoryService _compareHistoryService;

    public CoinController(ICoinMarketCapService coinService, ICoinRepository coinRepository, ICompareHistoryService compareHistoryService)
    {
        _coinService = coinService;
        _coinRepository = coinRepository;
        _compareHistoryService = compareHistoryService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCoins(CancellationToken cancellationToken = default)
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
    public async Task<IActionResult> CompareMarketCapOfTwoCoins(string coin1, string coin2, CancellationToken cancellationToken = default)
    {
        var firstCoin = await _coinService.GetByNameOrSymbolAsync(coin1, cancellationToken);
        var secondCoin = await _coinService.GetByNameOrSymbolAsync(coin2, cancellationToken);

        var aWithMarketCapB = firstCoin.MarketCap / secondCoin.TotalSupply;

        var result = new
        {
            ACoinSymbol = firstCoin.Symbol,
            BCoinSymbol = secondCoin.Symbol,
            ACoinWithMarketCapOfBCoin = aWithMarketCapB.ToString("F2"),
            NumberOfA_X = (aWithMarketCapB / secondCoin.Price).ToString("F2"),
            NumberOfB_X = (secondCoin.Price / aWithMarketCapB).ToString("F2")
        };

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            CompareHistory compareHistory = new CompareHistory 
            { 
                UserId = new Guid(userId),
                ACoinId = firstCoin.Id,
                BCoinId = firstCoin.Id,
                DateOfComaring = DateTime.UtcNow
            };
            await _compareHistoryService.CreateAsync(compareHistory, cancellationToken);
        }

        return Ok(result);
    }
}