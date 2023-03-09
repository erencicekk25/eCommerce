using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class City
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Şehir Adı")]
        public string Name { get; set; }

        public int? CountryID { get; set; }
        public Country Country { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
