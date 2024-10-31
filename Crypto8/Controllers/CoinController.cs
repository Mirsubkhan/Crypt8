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
    private readonly ICoinMarketCapService _coinMarketCapService;

    public CoinController(ICoinMarketCapService coinMarketCapService)
    {
        _coinMarketCapService = coinMarketCapService;
    }

    [HttpGet("Compare")]
    public async Task<IActionResult> GetMarketCapOf(string coinName1, string coinName2)
    {
        if (string.IsNullOrWhiteSpace(coinName1) || string.IsNullOrWhiteSpace(coinName2))
        {
            return BadRequest("Choose a cryptocurrency!");
        }

        Coin coin1 = await _coinMarketCapService.GetByNameAsync(coinName1);
        Coin coin2 = await _coinMarketCapService.GetByNameAsync(coinName2);

        return Ok(new {coin1, coin2});
    }
}