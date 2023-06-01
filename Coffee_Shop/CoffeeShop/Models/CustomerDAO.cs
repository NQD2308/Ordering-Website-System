namespace CoffeeShop.Models
{
    public class CustomerDAO
    {
        private OderDrinkingContext _ctx = new OderDrinkingContext();

        public Boolean CreateNewCustomer(Customer customer)
        {
            _ctx.Add(customer);
            _ctx.SaveChanges();

            return true;
        }

        public Boolean UpdateCustumer(Customer Updatecustomer)
        {
            Customer ctm = _ctx.Customers.Where(x=>x.Id== Updatecustomer.Id).FirstOrDefault();
            ctm.Name = Updatecustomer.Name;
            ctm.PhoneNumber = Updatecustomer.PhoneNumber;
            ctm.Address = Updatecustomer.Address;

            _ctx.Customers.Update(ctm);
            _ctx.SaveChanges();

            return true;
        }
    }
}
