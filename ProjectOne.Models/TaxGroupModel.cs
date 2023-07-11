using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class TaxGroupModel
    {
        public int Id { get; set; }

        public int? TaxTypeId { get; set; }

        public string? Name { get; set; }
        public IEnumerable<TaxGroupChildsModel> TaxGroupChildsList { get; set; }

        public int CompanyMasterId { get; set; }

        public int BranchMasterId { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int IsEditable { get; set; }

        public int IsActive { get; set; }
    }
}
