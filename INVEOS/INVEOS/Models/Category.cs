using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
