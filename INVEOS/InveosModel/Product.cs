using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Product
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
    }
}
