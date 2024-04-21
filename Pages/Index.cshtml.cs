using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace Learn_code.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly MyBlogContext _myblogContext;

    public IndexModel(ILogger<IndexModel> logger, MyBlogContext myblogContext)
    {
        _logger = logger;
        _myblogContext = myblogContext;
    }

    public void OnGet()
    {
        var posts = (from p in _myblogContext.Articles
                     orderby p.Created descending
                     select p).ToList();

        ViewData["posts"] = posts;
    }
}
