using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int CustomerId { get; set; }

    public string Address { get; set; } = null!;

    public string? Note { get; set; }

    public double? DiscountTotalBill { get; set; }

    public double TotalBill { get; set; }

    public double? FeeShip { get; set; }

    public string? CustomerReceiveName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? DateReceive { get; set; }

    public bool? Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<DetailBill> DetailBills { get; set; } = new List<DetailBill>();
}
