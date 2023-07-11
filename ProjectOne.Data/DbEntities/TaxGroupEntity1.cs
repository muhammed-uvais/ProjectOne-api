using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.DbEntities
{
    public class TaxGroupEntity1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TaxTypeId { get; set; }
        public string TaxTypeName { get; set; }
        public int IsEditable { get; set; }
        public int IsActive { get; set; }
        public string CreatedDateString { get; set; }
        public IEnumerable<TaxGroupChildsEntity1> TaxGroupChildsList { get; set; }
        public decimal Amount { get; set; }
    }
}
