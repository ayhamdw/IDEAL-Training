using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string PageUrl { get; set; } = null!;

    public string? PageName { get; set; }

    public string PermissionKey { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }
}
