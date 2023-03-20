using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_pages.Pages.Book;

public class Index : PageModel
{
    public static readonly string TITLE = "title";
    private ILogger _logger;
    

    public Index(ILogger<Index> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public List<string> Books { get; set; }

    public IActionResult OnGet(int id)
    {
        
        Books = new List<string>() {"Java", "C#", "Python"};
        _logger.LogInformation("onGet with id " + id);
        return Page();
    }
    public IActionResult OnGetAll()
    {
        Books = new List<string>() {"Java", "C#", "Python", "JavaScript", "PHP"};
        _logger.LogInformation("onGetAll ");
        return Page();
    }
    
    public class IndexModel : PageModel
    {
        [ViewData]
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Hello World";
        }
    }
    

    
}