using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class AspNetUserRole
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;
}
