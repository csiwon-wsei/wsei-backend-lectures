using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using NuGet.Protocol;
using web_api_jwt.Data;
using web_api_jwt.Models;

namespace web_api_jwt.Controllers;

[ApiController, Route("/api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _manager;
    private readonly ILogger _logger;

    public AuthenticationController(UserManager<IdentityUser> manager, ILogger<AuthenticationController> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized();
        }
        
        var logged = await _manager.FindByNameAsync(user.LoginName);
        var check = await _manager.CheckPasswordAsync(logged, user.Password);
        if (check)
        {
            return Ok(new {Token = await CreateToken()});
        }
        return Unauthorized();
    }

    private async Task<string> CreateToken()
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA512Algorithm())
            .WithSecret("12345")
            .AddClaim("username", "karol")
            .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
            .Audience("access")
            .Issuer("WseiBackend")
            .Encode();
    }
    [AcceptVerbs("GET", "POST")]
    [Produces("application/json")]
    public async Task<string> InvalidateToken()
    {
        return "";
    }
}