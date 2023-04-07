using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_jwt.Data;
using web_api_jwt.Models;

namespace web_api_jwt.Controllers;

[ApiController]
[Route("/api/books")]
public class BookController: ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private ILogger _logger;

    public BookController(AppDbContext context, ILogger<BookController> logger, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet] 
    [Authorize(Policy = "Bearer")]
    [Authorize(Policy = "Email")]
    public async Task<List<Book>> GetBooks()
    {
        var user = await GetCurrentUser();
        _logger.LogInformation($"USER: {user}");
        var books = _context.Books.ToList();
        return books;
    }
    
    private async Task<IdentityUser?> GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            string name = identity.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
            return await _userManager.FindByNameAsync(name);
        }
        return null;
    }
}