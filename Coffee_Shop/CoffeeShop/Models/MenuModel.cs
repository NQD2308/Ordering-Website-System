using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeeShop.Models
{
    public class MenuModel
    {
        public List<Product> coffee { get; set; }
        public List<Product> fruit { get; set; }
        public List<Product> tea { get; set; }
        public List<Product> MilkShake { get; set; }

        public CartModel Cart { get; set; }
        public Product product { get; set; }
        public Bill bill { get; set; }
    }
}
