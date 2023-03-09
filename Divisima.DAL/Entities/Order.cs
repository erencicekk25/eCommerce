using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Divisima.DAL.Entities
{
    public class Order
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(20)"),StringLength(20),Display(Name ="Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [Display(Name = "Ödeme Seçeneği")]
        public EPaymentOption PaymentOption { get; set; }

        [Display(Name = "Sipariş Durumu")]
        public EOrderStatus OrderStatus { get; set; }

        [Display(Name = "Sipariş Tarihi")]
        public DateTime RecDate { get; set; }

        [Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Teslimat Adresi")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Şehri")]
        public string City { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat İlçe")]
        public string Distinct { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Teslimat Ülkesi")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(10)"), StringLength(10), Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Column(TypeName = "varchar(80)"), StringLength(80), Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Column(TypeName = "varchar(80)"), StringLength(80), Display(Name = "Adı Soyadı")]
        public string NameSurname { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Kargo Ücreti")]
        public decimal ShipmentFee { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string CardNumber { get; set; }
        [NotMapped]
        public string CardMonth { get; set; }
        [NotMapped]
        public string CardYear { get; set; }
        [NotMapped]
        public string CardCV2 { get; set; }
    }
}
