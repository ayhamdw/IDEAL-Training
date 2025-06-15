using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class SystemLog
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Component { get; set; } = null!;

    public string? StackTrace { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }
}
