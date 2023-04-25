using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Phone { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Product>? Products { get; } = new List<Product>();

    public virtual ICollection<Purchaseorderheader>? Purchaseorderheaders { get; } = new List<Purchaseorderheader>();
}
