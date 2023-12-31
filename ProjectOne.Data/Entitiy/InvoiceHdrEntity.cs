﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.Entitiy
{
    public class InvoiceHdrEntity
    {
        public int Id { get; set; }
        public int? InvoiceCustomerDetailsId { get; set; }
        public int? DisableTrn { get; set; }
        public long Number { get; set; }
        public string NumberDisplay { get; set; } = null!;
        public string? CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int IsActive { get; set; }

        public InvoiceCustomerDetailEntity CustomerDetails { get; set; }

        public IEnumerable<InvoiceContentEntity> InvoiceItems { get; set; }
        public InvoiceAmountEntity InvoiceAmount { get; set; }
    }
}
