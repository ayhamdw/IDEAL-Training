using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class SystemFile
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string FileUrl { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime ModifiedOn { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public string? AltText { get; set; }
}
