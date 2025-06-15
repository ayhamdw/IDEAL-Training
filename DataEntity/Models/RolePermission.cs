using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public int PermissionId { get; set; }
}
