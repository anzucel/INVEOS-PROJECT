using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public int Address { get; set; }

    public string Email { get; set; } = null!;

    public int Phone { get; set; }

    public virtual Address AddressNavigation { get; set; } = null!;

    public virtual ICollection<Saleorderheader> Saleorderheaders { get; } = new List<Saleorderheader>();
}
