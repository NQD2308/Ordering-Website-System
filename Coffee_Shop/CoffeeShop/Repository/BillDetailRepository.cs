using CoffeeShop.Models;

namespace CoffeeShop.Repository
{
    public interface IBillDetailRepository
    {
        public bool Create(DetailBill detailBill);
    }
    public class BillDetailRepository : IBillDetailRepository
    {
        private OderDrinkingContext _ctx;
        public BillDetailRepository(OderDrinkingContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(DetailBill detailBill)
        {
            _ctx.DetailBills.Add(detailBill);
            _ctx.SaveChanges();
            return true;
        }
    }
}
