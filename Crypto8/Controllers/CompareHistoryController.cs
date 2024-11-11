using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Crypt8.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CompareHistoryController: ControllerBase
{
    private readonly ICompareHistoryService _compareHistoryService;

    public CompareHistoryController(ICompareHistoryService compareHistoryService)
    {
        _compareHistoryService = compareHistoryService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllHistory(CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var allHistory = await _compareHistoryService.GetAllAsync(new Guid(userId), cancellationToken = default);

        if (allHistory == null || !allHistory.Any())
        {
            Console.WriteLine("No history found in database.");
            return BadRequest("No history found in database.");
        }

        return Ok(allHistory);
    }

    [HttpGet("last")]
    public async Task<IActionResult> GetLastByHistory(string coin1, string coin2, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized("User ID not found in token.");
        }

        var last = await _compareHistoryService.GetLastAsync(new Guid(userId), cancellationToken);

        if (last == null)
        {
            Console.WriteLine("No history found in database.");
            return BadRequest("No history found in database.");
        }

        return Ok(last);
    }
}

