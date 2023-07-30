using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.Entitiy
{
    public class InvoiceCustomerDetailEntity
    {
        public int Id { get; set; }

        public int InvoiceHdrId { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Vatumber { get; set; }

        public int IsActive { get; set; }
    }
}
