using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public enum EPaymentOption
    {
        [Display(Name ="Kredi Kartı İle Ödeme")]
        KrediKartı=1,
        [Display(Name = "Havale/EFT İle Ödeme")]
        Havale,
        [Display(Name = "Kapıda Nakit/Kredi Kartı İle Ödeme")]
        KapıdaÖdeme
    }
}
