using System;
using System.Collections.Generic;

namespace DataEntity.Models;

public partial class UserProfile
{
    public int Id { get; set; }


    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime CreatedOn { get; set; }

    public int Status { get; set; }

    public DateTime? DeletedOn { get; set; }

    public DateTime? LastLogin { get; set; }
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
