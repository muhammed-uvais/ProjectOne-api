using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class CompanyMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string VatNumber { get; set; } = null!;

    public int IsDefault { get; set; }

    public int IsActive { get; set; }
}
