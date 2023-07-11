using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class CompanyMasterLocalization
{
    public int Id { get; set; }

    public int CompanyMasterId { get; set; }

    public string? Name { get; set; }

    public string? AddressFirst { get; set; }

    public string? AddressSecond { get; set; }

    public string? PinCode { get; set; }

    public string? LanguageMasterCode { get; set; }

    public int IsActive { get; set; }
}
