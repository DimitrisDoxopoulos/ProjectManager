using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class User: BaseModel
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Employee> Contacts { get; set; } = new List<Employee>();

    public virtual ICollection<Project> Tasks { get; set; } = new List<Project>();
}
