﻿using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class InvoiceContent
{
    public int Id { get; set; }

    public int InvoiceHdrId { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public long? QtyPerDay { get; set; }

    public int? Vatpercentage { get; set; }

    public decimal? TaxableValue { get; set; }

    public decimal? Vatamount { get; set; }

    public decimal? TotalAmount { get; set; }

    public int IsActive { get; set; }
}