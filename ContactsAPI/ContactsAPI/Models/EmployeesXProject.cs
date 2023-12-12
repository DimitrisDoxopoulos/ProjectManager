using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class EmployeesXProject
{
    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
