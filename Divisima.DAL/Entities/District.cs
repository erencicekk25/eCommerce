using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class District
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "İlçe Adı")]
        public string Name { get; set; }

        public int? CityID { get; set; }
        public City City { get; set; }
    }
}
