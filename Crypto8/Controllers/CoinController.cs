using BusinessLogic.Services.Interfaces;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories.Entities;
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
        return Ok(coins);
    }

    [HttpGet("compare")]
    public async Task<IActionResult> CompareCoins(string coin1, string coin2, CancellationToken cancellationToken)
    {
        var firstCoin = await _coinService.GetByNameAsync(coin1, cancellationToken);
        var secondCoin = await _coinService.GetByNameAsync(coin2, cancellationToken);

        if (firstCoin == null || secondCoin == null)
        {
            return NotFound("One or both of the coins were not found.");
        }

        return Ok(new
        {
            Coin1 = firstCoin,
            Coin2 = secondCoin
        });
    }
}