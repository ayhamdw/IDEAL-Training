using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class SystemSettingTranslation
{
    public int Id { get; set; }

    public int SettingId { get; set; }

    public int LanguageId { get; set; }

    public bool IsDefault { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;
}
