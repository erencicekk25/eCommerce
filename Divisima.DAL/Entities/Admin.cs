using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class Admin
    {
        public int ID { get; set; } //Id ID id

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"),Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(32)"), StringLength(32), Display(Name = "Şifre"),Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Admin Adı Soyadı"), Required(ErrorMessage = "Ad Soyad Boş Geçilemez")]
        public string NameSurname { get; set; }

        [Display(Name = "Son Giriş Tarihi")]
        public DateTime LastLoginDate { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Son Giriş IP No")]
        public string LastLoginIP { get; set; }

    }
}
