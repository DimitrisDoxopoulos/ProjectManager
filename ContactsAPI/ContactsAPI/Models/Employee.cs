using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class Employee: BaseModel
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Email { get; set; }

    public string? CompanyRole { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual User User { get; set; } = null!;
}
