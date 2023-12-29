using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class Employee: BaseModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Email { get; set; }

    public string? CompanyRole { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; }

    public ICollection<EmployeeProject> EmployeeProjects { get; set; }
}
