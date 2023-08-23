﻿using System;
using System.Collections.Generic;

namespace ProjectOne.Data.DbEntities;

public partial class InvoiceCustomerDetail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Vatumber { get; set; }

    public int IsActive { get; set; }
}
