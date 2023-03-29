using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveosModel
{
    public class Employee
    {
        //public int EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? SecondName { get; set; }

        public string FirstLastname { get; set; } = null!;

        public string SecondLastname { get; set; } = null!;

        public int Phone { get; set; }

        public string Email { get; set; } = null!;

        public sbyte Gender { get; set; }

        public int Role { get; set; }

        public int? Address { get; set; }
        public Address? AddressNavigation { get; set; } = null!;
    }
}
