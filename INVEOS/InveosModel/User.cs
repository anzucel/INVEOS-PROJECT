using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class User
    {
        public int? UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public sbyte Status { get; set; }

        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; } = null!;
    }
}
