using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using DataAccess.Models;

namespace Crypt8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User { UserName = model.Username, Email = model.Email };
        var res = await _userManager.CreateAsync(user, model.Password);

        if (res.Succeeded)
        {
            return Ok(new { message = "User created successfully" });
        }

        return BadRequest(res.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = GenerateJwtToken(user);
            Response.Cookies.Append("nekoThtuA", token, new CookieOptions 
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30) 
            });

            return Ok(new { token });
        }

        return Unauthorized();
    }
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],    
            claims: claims,                 
            expires: DateTime.UtcNow.AddDays(30),   
            signingCredentials: creds 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

