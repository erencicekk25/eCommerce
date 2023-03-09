using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(100)"),StringLength(100),Display(Name ="Ürün Adı"), Required(ErrorMessage = "Ürün Adı Boş Geçilemez")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Fiyat Bilgisi")]
        public decimal Price { get; set; }

        [Display(Name = "Stok Miktarı")]
        public int Stock { get; set; }

        [Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }

        [Column(TypeName = "text"), Display(Name = "Ürün Detayı")]
        public string Detail { get; set; }

        [Column(TypeName = "text"), Display(Name = "Kargo Detayı")]
        public string CargoDetail { get; set; }

        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

        public ICollection<ProductPicture> ProductPictures { get; set; }

        [Display(Name = "Markası")]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }

        [Display(Name = "Kategorisi")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
