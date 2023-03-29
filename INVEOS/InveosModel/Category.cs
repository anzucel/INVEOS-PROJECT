using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public byte[]? Image { get; set; }
    }
}
