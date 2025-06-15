using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class SystemSetting
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public bool? ShowInTheDashboard { get; set; }
}
