using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class InvoiceAmountModel
    {
        public int Id { get; set; }

        public int InvoiceHdrId { get; set; }

        public decimal? TaxableValue { get; set; }
        public decimal? Vatexcludedamount { get; set; }

        public decimal? Vatamount { get; set; }

        public decimal? TotalAmount { get; set; }

        public int IsActive { get; set; }
    }
}
