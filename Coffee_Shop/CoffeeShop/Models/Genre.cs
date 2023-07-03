using System;
using System.Collections.Generic;

namespace CoffeeShop.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
