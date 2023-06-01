using CoffeeShop.Models;

namespace CoffeeShop.Repository
{
    public interface IProductRepository
    {
        public bool Create(Product product);
        public bool Update(Product product);
        public bool Delete(string product);
        public List<Product> GetAll();
        public Product FindById(string Id);

        public bool CheckId(string id);
        public bool CheckNameProduct(string name);
        public List<Product> FindAllProductById(int id);

    }
    public class ProductRepository : IProductRepository
    {
        private OderDrinkingContext _ctx;
        public ProductRepository(OderDrinkingContext ctx)
        {
            _ctx = ctx;
        }

        public bool CheckId(string id)
        {
            Product p = _ctx.Products.Where(x => x.ProductId.Trim() == id.Trim()).FirstOrDefault();
            if(p == null)
                return false;
            else
                return true;
        }

        public bool CheckNameProduct(string name)
        {
            Product c = _ctx.Products.Where(x=>x.ProductName.Trim() == name.Trim()).FirstOrDefault();
            if(c == null)
                return false;
            else
                return true;
        }

        public bool Create(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(string Id)
        {
            Product c = _ctx.Products.FirstOrDefault(x => x.ProductId == Id);
            _ctx.Products.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

        public List<Product> FindAllProductById(int id)
        {
            return _ctx.Products.Where(x=>x.GenreId == id).ToList();
        }

        public Product FindById(string id)
        {
            Product c = _ctx.Products.FirstOrDefault(x => x.ProductId == id);
            return c;
        }

        public List<Product> GetAll()
        {
            return _ctx.Products.ToList();
        }

        public bool Update(Product product)
        {
            Product c = _ctx.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(product);
                _ctx.SaveChanges();
            }
            return true;
        }
    }
}
