using System;
using System.Collections.Generic;

namespace INVEOSAPI.Models;

public partial class Employee
{
    public int? EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string FirstLastname { get; set; } = null!;

    public string SecondLastname { get; set; } = null!;

    public int Phone { get; set; }

    public string Email { get; set; } = null!;

    public sbyte Gender { get; set; }

    public int Role { get; set; }

    public int? Address { get; set; }

    public virtual Address AddressNavigation { get; set; } = null!;

    public virtual ICollection<Purchaseorderheader> Purchaseorderheaders { get; } = new List<Purchaseorderheader>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Saleorderheader> Saleorderheaders { get; } = new List<Saleorderheader>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
