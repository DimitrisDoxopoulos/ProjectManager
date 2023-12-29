using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class EmployeeProject : BaseModel
{
    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
