using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class UserLogin
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long UserMasterId { get; set; }

    public int? IsAdmin { get; set; }

    public int? IsActive { get; set; }
}
