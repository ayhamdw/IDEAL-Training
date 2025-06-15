using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class DataBaseScript
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }
}
