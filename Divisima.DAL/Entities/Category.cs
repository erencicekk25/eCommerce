using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Üst Kategori Adı")]
        public int? ParentID { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }


        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
