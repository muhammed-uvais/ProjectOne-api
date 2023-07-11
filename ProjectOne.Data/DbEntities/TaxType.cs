using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class TaxType
{
    public int Id { get; set; }

    public string TaxType1 { get; set; } = null!;

    public int CompanyMasterId { get; set; }

    public int BranchMasterId { get; set; }

    public long ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int IsEditable { get; set; }

    public int IsActive { get; set; }
}
