using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class DetailsLookupTranslation
{
    public int Id { get; set; }

    public int DetailsLookupId { get; set; }

    public string Value { get; set; } = null!;

    public int LanguageId { get; set; }

    public bool IsDefault { get; set; }
}
