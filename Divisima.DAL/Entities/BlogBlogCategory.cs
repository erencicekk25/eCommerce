using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class BlogBlogCategory
    {
        public int BlogID { get; set; }
        public Blog Blog { get; set; }

        public int BlogCategoryID { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
