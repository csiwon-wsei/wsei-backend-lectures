﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_jwt.Data;
using web_api_jwt.Models;

namespace web_api_jwt.Controllers;

[ApiController, Route("/api/v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _manager;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger _logger;

    public AuthenticationController(UserManager<IdentityUser> manager, ILogger<AuthenticationController> logger, IConfiguration configuration, JwtSettings jwtSettings)
    {
        _manager = manager;
        _logger = logger;
        _jwtSettings = jwtSettings;
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
        if (await _manager.CheckPasswordAsync(logged, user.Password))
        {
            return Ok(new {Token = CreateToken(logged)});
        }
        return Unauthorized();
    }

    private string CreateToken(IdentityUser user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Gender, "male")
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
    
    [AcceptVerbs("GET", "POST")]
    [Produces("application/json")]
    public async Task<string> InvalidateToken()
    {
        return "";
    }
}