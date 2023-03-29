using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Purchaseorderheader
    {
        public int PurchaseOrderHeaderId { get; set; }

        public int PurchaseNumber { get; set; }

        public int EmployeeId { get; set; }

        public int SupplierId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? TaxAmt { get; set; }

        public decimal Total { get; set; }

        public string? Comment { get; set; }

        public sbyte Status { get; set; }
    }
}
