using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class InvoiceHdrModel
    {
        public int Id { get; set; }
        public long Number { get; set; }
        public string NumberDisplay { get; set; } = null!;
        public DateTime EntryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CustomerName { get; set; }
        public int IsActive { get; set; }
         
        public InvoiceCustomerDetailModel CustomerDetails { get; set; }
        public IEnumerable<InvoiceContentModel> InvoiceItems { get; set; }
    }
}
