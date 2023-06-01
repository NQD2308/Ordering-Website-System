using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models;

public partial class Product
{
    [Required]
    [StringLength(10)]
    public string ProductId { get; set; } = null!;

    [Required]
    public string ProductName { get; set; } = null!;

    public int GenreId { get; set; }

    public double Price { get; set; }

    public string? Describe { get; set; }

    public double? Discount { get; set; }

    [Required]
    public string? ImgPath { get; set; }

    public int? QuantitySaler { get; set; }

    public string? ImgPhoto { get; set; }

    public virtual ICollection<DetailBill> DetailBills { get; set; } = new List<DetailBill>();

    public virtual Genre? Genre { get; set; } = null!;
}
