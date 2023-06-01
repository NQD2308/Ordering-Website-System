using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    [Required]
    public string GenreName { get; set; } = null!;

    public virtual ICollection<HistoryBill> HistoryBills { get; set; } = new List<HistoryBill>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
