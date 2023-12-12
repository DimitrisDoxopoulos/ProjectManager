using ContactsAPI.Models;
using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class Employee : BaseModel
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Email { get; set; }

    public string? CompanyRole { get; set; }

    public string? Slug { get; set; }

    public virtual User User { get; set; } = null!;
}
