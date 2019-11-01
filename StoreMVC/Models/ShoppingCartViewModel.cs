using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<StoreProduct> ListOfShoppingCartProducts { get; set; }
    }
}
