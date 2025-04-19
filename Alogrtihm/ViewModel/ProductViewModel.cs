using Algorithm.Controllers;
using Algorithm.Models;

namespace Alogrtihm.ViewModel
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
        public PriceRange PriceRange { get; set; }
    }
}
