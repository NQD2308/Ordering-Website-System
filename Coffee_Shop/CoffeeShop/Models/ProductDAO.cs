namespace CoffeeShop.Models
{
    public class ProductDAO
    {
        private OderDrinkingContext _ctx = new OderDrinkingContext();

        public List<Product> GetAllProduct()
        {
            return _ctx.Products.ToList();
        }

        //Chon loc du lieu dau vao
        public List<Product> GetCoffeeProduct()
        {
            return _ctx.Products.Where(x=>x.GenreId == 1).ToList();
        }

        public List<Product> GetFruitProduct()
        {
            return _ctx.Products.Where(x => x.GenreId == 2).ToList();
        }

        public List<Product> GetTeaProduct()
        {
            return _ctx.Products.Where(x => x.GenreId == 3).ToList();
        }

        public List<Product> GetMilkshakeProduct()
        {
            return _ctx.Products.Where(x => x.GenreId == 4).ToList();
        }
        public Boolean CreateProduct(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            
            return true;
        }

        public Boolean Update(Product Updateproduct)
        {
            Product pr = _ctx.Products.Where(x=>x.ProductId== Updateproduct.ProductId).FirstOrDefault();
            
            pr.Describe = Updateproduct.Describe;
            pr.Discount = Updateproduct.Discount;
            pr.Price= Updateproduct.Price;

            _ctx.Update(pr);
            _ctx.SaveChanges();
            
            return true;
        }

        public Boolean Delete(String ProductID)
        {
            Product pr = _ctx.Products.Where(x=>x.ProductId == ProductID).FirstOrDefault();
            
            _ctx.Products.Remove(pr);
            _ctx.SaveChanges();
            
            return true;
        }
    }
}
