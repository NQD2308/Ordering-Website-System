using CoffeeShop.Models;

namespace CoffeeShop.Repository
{
    public interface IBillRepository
    {
        public bool Create(Bill bill);
    }
    public class BillRepository : IBillRepository
    {
        private OderDrinkingContext _ctx;
        public BillRepository(OderDrinkingContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Bill bill)
        {
            _ctx.Bills.Add(bill);
            _ctx.SaveChanges();
            return true;
        }
    }
}
