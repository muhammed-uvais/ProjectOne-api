using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Trn { get; set; }

    public int IsActive { get; set; }
}
