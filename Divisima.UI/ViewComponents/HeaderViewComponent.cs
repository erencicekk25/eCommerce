using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        IRepository<Category> repoCategory;
        public HeaderViewComponent(IRepository<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }
        public IViewComponentResult Invoke()
        {
            return View(repoCategory.GetAll().Include(x=>x.SubCategories).OrderBy(x=>x.DisplayIndex));
        }
    }
}
