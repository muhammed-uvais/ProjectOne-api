using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class TaxGroup
{
    public int Id { get; set; }

    public int? TaxTypeId { get; set; }

    public string? Name { get; set; }

    public int CompanyMasterId { get; set; }

    public int BranchMasterId { get; set; }

    public long ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int IsEditable { get; set; }

    public int IsActive { get; set; }
}
