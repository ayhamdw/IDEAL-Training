using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class DetailsLookup
{
    public int Id { get; set; }

    public int MasterId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? Code { get; set; }

    public string? Value { get; set; }
}
