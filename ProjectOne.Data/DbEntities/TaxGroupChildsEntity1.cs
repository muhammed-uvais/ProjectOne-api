using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.DbEntities
{
    public class TaxGroupChildsEntity1
    {
        public int Id { get; set; }
        public int TaxGroupId { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int IsActive { get; set; }
    }
}
