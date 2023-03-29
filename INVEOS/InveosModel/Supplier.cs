using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Phone { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
    }
}
