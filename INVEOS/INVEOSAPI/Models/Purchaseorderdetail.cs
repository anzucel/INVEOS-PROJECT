using System;
using System.Collections.Generic;

namespace INVEOSAPI.Models;

public partial class Purchaseorderdetail
{
    public int PurchaseOrderDetailId { get; set; }

    public int PurchaseOrderHeaderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchaseorderheader PurchaseOrderHeader { get; set; } = null!;
}
