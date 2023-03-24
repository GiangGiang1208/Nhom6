using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using TinTucCongNghe.Helpper;
using TinTucCongNghe.Models;

namespace TinTucCongNghe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminPostsController : Controller
    {
        private readonly dbTinTucCongNgheContext _context;
        public INotyfService _notyfService { get; }
        public AdminPostsController(dbTinTucCongNgheContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminPosts
        public async Task<IActionResult> Index(int page = 1, int CatID = 0)
        {
            var pageNumber = page;
            var pageSize = 10;

            List<Post> lsPosts = new List<Post>();
            if (CatID != 0)
            {
                lsPosts = _context.Posts
                .AsNoTracking()
                .Where(x => x.CatId == CatID)
                .Include(x => x.Cat)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
            }
            else
            {
                lsPosts = _context.Posts
                .AsNoTracking()
                .Include(x => x.Cat)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
            }

            PagedList<Post> models = new PagedList<Post>(lsPosts.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentCateID = CatID;

            ViewBag.CurrentPage = pageNumber;

            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName");

            return View(models);
        }

        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/AdminPosts?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminPosts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        

        // GET: Admin/AdminPosts/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/AdminPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,CatId,Title,Scontent,Contents,Alias,IsPublish,IsHot,IsNew,HomeFlag,DateCreated,DateModified,Thumb")] Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(post.Title) + extension;
                    post.Thumb = await Utilities.UploadFile(fThumb, @"tintuc", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(post.Thumb)) post.Thumb = "default.jpg";
                post.Alias = Utilities.SEOUrl(post.Title);
                post.DateCreated = DateTime.Now;

                _context.Add(post);
                await _context.SaveChangesAsync();
                _notyfService.Success("Thêm tin thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", post.CatId);
            return View(post);
        }

        // GET: Admin/AdminPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", post.CatId);
            return View(post);
        }

        // POST: Admin/AdminPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,CatId,Title,Scontent,Contents,Alias,IsPublish,IsHot,IsNew,HomeFlag,DateCreated,DateModified,Thumb")] Post post, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(post.Title) + extension;
                        post.Thumb = await Utilities.UploadFile(fThumb, @"tintuc", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(post.Thumb)) post.Thumb = "default.jpg";
                    post.Alias = Utilities.SEOUrl(post.Title);
                    post.DateCreated = DateTime.Now;

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Lưu thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", post.CatId);
            return View(post);
        }

        // GET: Admin/AdminPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/AdminPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'dbTinTucCongNgheContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            _notyfService.Success("Đã xóa tin");
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
