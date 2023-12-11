using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class Project: BaseModel
{
    public int UserId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual User User { get; set; } = null!;
}
