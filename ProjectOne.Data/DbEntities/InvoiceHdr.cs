using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class InvoiceHdr
{
    public int Id { get; set; }

    public int? DisableTrn { get; set; }

    public int? InvoiceCustomerDetailsId { get; set; }

    public DateTime EntryDate { get; set; }

    public long Number { get; set; }

    public string NumberDisplay { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int IsActive { get; set; }
}
