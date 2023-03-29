using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Saleorderdetail
{
    public int SaleOrderDetailId { get; set; }

    public int SaleOrderHeaderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal Total { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Saleorderheader SaleOrderHeader { get; set; } = null!;
}
