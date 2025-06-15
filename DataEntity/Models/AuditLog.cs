using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class AuditLog
{
    public int Id { get; set; }

    public string Action { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }

    public string? RequestDetails { get; set; }
}
