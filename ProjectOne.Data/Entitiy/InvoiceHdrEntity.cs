using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.Entitiy
{
    public class InvoiceHdrEntity
    {
        public int Id { get; set; }
        public long Number { get; set; }
        public string NumberDisplay { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public int IsActive { get; set; }

        public IEnumerable<InvoiceContentEntity> InvoiceItems { get; set; }
    }
}
