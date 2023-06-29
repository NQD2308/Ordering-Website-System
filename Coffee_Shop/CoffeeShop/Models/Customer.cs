using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? HistoryBillId { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual HistoryBill? HistoryBill { get; set; }
}
