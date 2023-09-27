using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class InvoiceAmount
{
    public int Id { get; set; }

    public int InvoiceHdrId { get; set; }

    public decimal? TaxableValue { get; set; }

    public decimal? Vatexcludedamount { get; set; }

    public decimal? Vatamount { get; set; }

    public decimal? TotalAmount { get; set; }

    public int IsActive { get; set; }
}
