using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Learn_code.Pages_Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Models.MyBlogContext _context;

        public IndexModel(Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } = default!;

        public const int ITEMS_PER_PAGE = 5;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            // Article = await _context.Articles.ToListAsync();

            int totalArticles = await _context.Articles.CountAsync();

            countPages = (int)Math.Ceiling((double)totalArticles / ITEMS_PER_PAGE);

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > countPages)
            {
                currentPage = countPages;
            }

            var qr = (from a in _context.Articles
                      orderby a.Created descending
                      select a).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);

            if (!string.IsNullOrEmpty(searchString))
            {
                Article = qr.Where(s => s.Title.Contains(searchString)).ToList();
            }
            else
            {
                Article = await qr.ToListAsync();
            }
        }
    }
}
