using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public int GenreId { get; set; }

    public decimal Price { get; set; }

    public string? Describe { get; set; }

    public double? Discount { get; set; }

    public string? ImgPath { get; set; }

    public int? QuantitySaler { get; set; }

    public string? ImgPhoto { get; set; }

    public virtual ICollection<DetailBill> DetailBills { get; set; } = new List<DetailBill>();

    public virtual Genre? Genre { get; set; } = null!;
}
