using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TinTucCongNghe.Models;
using TinTucCongNghe.ModelViews;

namespace TinTucCongNghe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbTinTucCongNgheContext _context;

        public HomeController(ILogger<HomeController> logger, dbTinTucCongNgheContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewVM model = new HomeViewVM();

            var lsPosts = _context.Posts
                .AsNoTracking()
                .Where(x => x.IsPublish == true && x.HomeFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
            var lsCats = _context.Categories
                .AsNoTracking()
                .Where(x => x.IsPublish == true)
                .OrderBy(x => x.Ordering)
                .Take(2)
                .ToList();

            List<PostHomeVM> lsPostViews = new List<PostHomeVM>();
            foreach (var item in lsCats)
            {
                PostHomeVM postHome = new PostHomeVM();
                postHome.category = item;
                postHome.lsPosts = lsPosts.Where(x => x.CatId == item.CatId).ToList();
                lsPostViews.Add(postHome);
            }

            var lsTinHot = _context.Posts
                .AsNoTracking()
                .Where(x => x.IsPublish == true && x.IsHot == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(6)
                .ToList();

            var lsTinMoi1 = _context.Posts
                .AsNoTracking()
                .Where(x => x.IsPublish == true && x.IsNew == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(2)
                .ToList();
            var lsTinMoi2 = _context.Posts
                .AsNoTracking()
                .Where(x => x.IsPublish == true && x.IsNew == true)
                .OrderByDescending(x => x.DateCreated)
                .Skip(2)
                .Take(2)
                .ToList();

            var quangcao = _context.Advs
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(5)
                .ToList();

            model.Posts = lsPostViews;
            model.QuangCaos = quangcao;
            model.TinHots = lsTinHot;
            model.TinMois1 = lsTinMoi1;
            model.TinMois2 = lsTinMoi2;
            ViewBag.AllPosts = lsPosts;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}