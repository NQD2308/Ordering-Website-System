using CoffeeShop.Models;

namespace CoffeeShop.Repository
{
    public interface ICustomerRepository
    {
        public bool Create(Customer customer);
        public bool Update(Customer UpdateCustomer);
        public bool Delete(int id);
        public List<Customer> GetAll();
        public Customer FindById(int id);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private OderDrinkingContext _ctx;

        public CustomerRepository(OderDrinkingContext ctx)
        {
            _ctx = ctx;
        }

        public bool Create(Customer customer)
        {
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.Id == id);
            _ctx.Customers.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

        public Customer FindById(int id)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.Id == id);
            return c;
        }

        public List<Customer> GetAll()
        {
            return _ctx.Customers.ToList();
        }

        public bool Update(Customer UpdateCustomer)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.Id == UpdateCustomer.Id);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(UpdateCustomer);
                _ctx.SaveChanges();
            }
            return true;
        }
    }
}
