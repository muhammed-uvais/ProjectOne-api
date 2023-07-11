using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class CompanyMasterBankDetail
{
    public int Id { get; set; }

    public int CompanyMasterId { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountName { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? Ibannumber { get; set; }

    public string? BankIfsc { get; set; }

    public int IsActive { get; set; }
}
