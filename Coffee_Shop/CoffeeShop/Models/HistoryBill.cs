using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class HistoryBill
{
    public string BillId { get; set; } = null!;

    public string? ProductsName { get; set; }

    public int? GenreId { get; set; }

    public double? Price { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Genre? Genre { get; set; }
}
