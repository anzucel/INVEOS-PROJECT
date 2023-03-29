using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;

        public int Address { get; set; }

        public string Email { get; set; } = null!;

        public int Phone { get; set; }
    }
}
