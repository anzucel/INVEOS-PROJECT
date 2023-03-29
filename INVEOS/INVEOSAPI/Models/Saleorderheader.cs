using System;
using System.Collections.Generic;

namespace INVEOSAPI.Models;

public partial class Saleorderheader
{
    public int SaleOrderId { get; set; }

    public int CustomerId { get; set; }

    public int SalesPersonId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public sbyte SaleStatus { get; set; }

    public sbyte PaymentStatus { get; set; }

    public decimal SubTotal { get; set; }

    public decimal? TaxAmt { get; set; }

    public decimal Total { get; set; }

    public string? Comment { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Saleorderdetail> Saleorderdetails { get; } = new List<Saleorderdetail>();

    public virtual Employee SalesPerson { get; set; } = null!;
}
