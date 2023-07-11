using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class MenuMaster
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int IsActive { get; set; }
}
