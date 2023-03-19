using TinTucCongNghe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CongNghePhanMem.Controllers.Components
{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly dbTinTucCongNgheContext _context;
        public MenuViewComponent(dbTinTucCongNgheContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = _context.Menus.Where(x => x.IsActive == true && x.Position == 1)
                .ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));
        }
    }
}

