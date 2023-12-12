using ContactsAPI.Models;
using System;
using System.Collections.Generic;

namespace ContactsAPI.Models;

public partial class Project : BaseModel
{
    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Slug { get; set; }

    public virtual User User { get; set; }
}
