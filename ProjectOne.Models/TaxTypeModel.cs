using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class TaxTypeModel
    {
        public int Id { get; set; }
        public string TaxType1 { get; set; }
        public int IsEditable { get; set; }
        public int IsActive { get; set; }
    }
}
