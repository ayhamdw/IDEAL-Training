using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class UserProfileTranslation
{
    public int Id { get; set; }

    public int UserProfileId { get; set; }

    public string FullName { get; set; } = null!;

    public int LanguageId { get; set; }


    public virtual UserProfile UserProfile { get; set; } = null!;
}
