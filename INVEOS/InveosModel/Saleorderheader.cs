using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Saleorderheader
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
    }
}
