namespace CoffeeShop.Models
{
    public class CartModel
    {
        public string NoteBill { get; set; }


        private List<Item> _items = new List<Item>();
        public string CartId { get; set; }
        public decimal Total { get; set; }
        public int GetCountItem()
        {
            return _items.Count;
        }
        public decimal getTotal()
        {
            decimal t = 0;
            foreach (var it in _items)
            {
                t += it.lineTotal;
            }
            return t;
        }
        public int addItem(Item item)
        {
            foreach (var it in _items)
            {
                decimal PriceByDiscount = 0;
                decimal dis = Convert.ToDecimal(it.Discount);
                PriceByDiscount = it.Price - (it.Price * (dis / 100));

                if (it.Id == item.Id)
                {
                    it.Quantity += item.Quantity;
                    it.lineTotal = it.Quantity * PriceByDiscount;

                    return _items.Count;
                }
            }
            _items.Add(item);

            //total
            foreach (var it in _items)
            {
                Total += it.lineTotal;
            }

            return _items.Count;
        }
        public void UpdateQuantity(string id, int qty, string btnCmd)
        {
            foreach (Item it in _items)
            {
                decimal PriceByDiscount = 0;
                decimal dis = Convert.ToDecimal(it.Discount);
                PriceByDiscount = it.Price - (it.Price * (dis / 100));
                if (it.Id == id)
                {
                    if (btnCmd == "+")
                    {
                        it.Quantity += qty;
                        it.lineTotal = it.Quantity * PriceByDiscount;
                    }
                    else
                    {
                        it.Quantity -= qty;
                        it.lineTotal = it.Quantity * PriceByDiscount;
                    }
                }
            }
            Total = 0;
            foreach (var it in _items)
            {
                Total += it.lineTotal;
            }
        }
        public List<Item> getAllItem()
        {
            return _items; 
        }
        public void setAllItems(List<Item> items)
        {
            _items = items;
        }
    }
}
