using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class MasterLookupTranslation
{
    public int Id { get; set; }

    public int MasterLookupId { get; set; }

    public string Name { get; set; } = null!;

    public int LanguageId { get; set; }

    public bool IsDefault { get; set; }
}
