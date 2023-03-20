using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_pages.Pages.Book;

public class Age : PageModel
{
    private readonly ILogger _logger;

    public Age(ILogger<Age> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public int BookAge { get; set; }
    
    public string Title { get; set; }

    public IActionResult OnGet(string title)
    {
        _logger.LogInformation("Path variable " + title);
        DateOnly? editionYear = DateOnly.Parse("2000-10-10");
        if (!editionYear.HasValue)
        {
            return BadRequest();
        }
        BookAge = DateOnly.FromDateTime(DateTime.Now).Year - editionYear.Value.Year;
        return Page();
    }

    public IActionResult onGetAll()
    {
        BookAge = 100;
        return Page();
    }
}