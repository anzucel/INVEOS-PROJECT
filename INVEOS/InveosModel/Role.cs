using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
