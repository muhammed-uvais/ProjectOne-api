using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class InvoiceItem
{
    public int Id { get; set; }

    public string NumberDisplay { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public long? QtyPerDay { get; set; }

    public int? Vatpercentage { get; set; }

    public decimal? TaxableValue { get; set; }

    public decimal? Vatamount { get; set; }

    public decimal TotalAmount { get; set; }

    public int IsActive { get; set; }
}
