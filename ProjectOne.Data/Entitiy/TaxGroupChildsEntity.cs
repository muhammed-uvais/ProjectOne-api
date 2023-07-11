using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Data.Entitiy
{
    public class TaxGroupChildsEntity
    {
        public int Id { get; set; }

        public int TaxGroupId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Rate { get; set; }

        public int IsActive { get; set; }
    }
}
