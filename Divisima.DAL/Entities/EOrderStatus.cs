using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public enum EOrderStatus
    {
        Hazırlanıyor,
        KargoVerildi,
        Dağıtımda,
        TeslimEdildi,
        İptalEdildi
    }
}
