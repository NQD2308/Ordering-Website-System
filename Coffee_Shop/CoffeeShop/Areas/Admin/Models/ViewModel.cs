using CoffeeShop.Models;

namespace CoffeeShop.Areas.Admin.Models
{
    public class ViewModel
    {
        public List<Product> products { get; set; }
        public List<Genre> genres { get; set; }
    }
}
