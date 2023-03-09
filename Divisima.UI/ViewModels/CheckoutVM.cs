using Divisima.DAL.Entities;
using Divisima.UI.Models;

namespace Divisima.UI.ViewModels
{
    public class CheckoutVM
    {
        public Order Order { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}
