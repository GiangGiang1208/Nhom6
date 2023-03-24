using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinTucCongNghe.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.Areas.Admin.Controllerst
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbTinTucCongNgheContext _context;

        public SearchController(dbTinTucCongNgheContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult FindPost(string keyword)
        {
            List<Post> ls = new List<Post>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListPostsSearchPartial", null);
            }
            ls = _context.Posts.AsNoTracking()
                                  .Include(a => a.Cat)
                                  .Where(x => x.Title.Contains(keyword))
                                  .OrderByDescending(x => x.Title)
                                  .Take(10)
                                  .ToList();
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }
    }
}
