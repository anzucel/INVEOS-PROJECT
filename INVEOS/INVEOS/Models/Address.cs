using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string Department { get; set; } = null!;

    public string Municipality { get; set; } = null!;

    public int Zone { get; set; }

    public string AddressDetail { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<Supplier> Suppliers { get; } = new List<Supplier>();
}
