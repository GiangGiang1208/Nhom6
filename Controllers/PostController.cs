using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;
using TinTucCongNghe.Models;

namespace TinTucCongNghe.Controllers
{
    public class PostController : Controller
    {
        private readonly dbTinTucCongNgheContext _context;
        public PostController(dbTinTucCongNgheContext context)
        {
            _context = context;
        }
        

        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 8;
                var lsPost = _context.Posts.AsNoTracking()
                    .Where(x => x.IsPublish == true && x.CatId == 1)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Post> models = new PagedList<Post>(lsPost, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                var category = _context.Categories.Where(x => x.IsPublish == true).ToList();
                ViewBag.Categories = category;
                return View(models);
            }
            catch
            {
                return NotFound();
            }

        }

        [Route("{Alias}-{id}.html", Name = "PostDetails")]
        public IActionResult Details(int id)
        {
            var post = _context.Posts.AsNoTracking().SingleOrDefault(x => x.PostId == id);

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 8;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);

                var lsPost = _context.Posts
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Post> models = new PagedList<Post>(lsPost, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                var category = _context.Categories.Where(x => x.IsPublish).ToList();
                ViewBag.Categories = category;
                return View(models);
            }
            catch
            {
                return NotFound();
            }

        }
    }
}
