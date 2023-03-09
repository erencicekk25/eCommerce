using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class ProductPicture
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Ürün Resim Adı"), Required(ErrorMessage = "Ürün Resim Adı Boş Geçilemez")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Resim Dosyası")]
        public string Picture { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

        [Display(Name = "Ürün Adı")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
