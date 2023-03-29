using System;
using System.Collections.Generic;

namespace INVEOSAPI.Models;

public partial class Product
{
    public int Idproduct { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Description { get; set; } = null!;

    public sbyte Status { get; set; }

    public int SuplierId { get; set; }

    public byte[]? Image { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; } = new List<Purchaseorderdetail>();

    public virtual ICollection<Saleorderdetail> Saleorderdetails { get; } = new List<Saleorderdetail>();

    public virtual Supplier Suplier { get; set; } = null!;
}
