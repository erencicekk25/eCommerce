using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class Blog
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(100)"),StringLength(100),Display(Name ="Blog Sayfası Başlığı")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Blog Sayfası Açıklaması")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Resim Dosyası")]
        public string Picture { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime RecDate { get; set; }

        public ICollection<BlogBlogCategory> BlogBlogCategories { get; set; }
    }
}
