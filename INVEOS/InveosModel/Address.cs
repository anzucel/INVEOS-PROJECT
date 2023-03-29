using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Address
    {
        //public int AddressId { get; set; }

        public string Department { get; set; } = null!;

        public string Municipality { get; set; } = null!;

        public int Zone { get; set; }

        public string AddressDetail { get; set; } = null!;
    }
}
