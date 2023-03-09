using Divisima.DAL.Entities;

namespace Divisima.UI.Areas.admin.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
