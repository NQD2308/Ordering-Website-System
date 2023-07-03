using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class DetailBill
{
    public int DetailId { get; set; }

    public int BillId { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public decimal Total { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
