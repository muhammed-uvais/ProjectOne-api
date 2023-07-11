using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class TaxGroupChild
{
    public int Id { get; set; }

    public int TaxGroupId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public int IsActive { get; set; }
}
