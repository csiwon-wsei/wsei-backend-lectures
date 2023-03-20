using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_pages.Pages.Book.Authors;

public class Index : PageModel
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public string Title { get; set; }
    public void OnGet(int year, int month, int day, string title)
    {
        Year = year;
        Month = month;
        Day = day;
        Title = title;
    }
    
    public class LinkGeneratorDemoModel : PageModel
    {
        private LinkGenerator linkGenerator;
        public LinkGeneratorDemoModel(LinkGenerator linkGenerator) => this.linkGenerator = linkGenerator;
        public string PathByPage { get; set; }
        public string UriByPage{ get; set; }
        public void OnGet()
        {
            PathByPage = linkGenerator.GetPathByPage("/Supplier", null, new { id = 2 });
            UriByPage = linkGenerator.GetUriByPage(HttpContext, "/Supplier", null, new { id = 2 });
        }
    }
}