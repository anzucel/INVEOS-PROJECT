using System;
using System.Collections.Generic;

namespace INVEOS.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
