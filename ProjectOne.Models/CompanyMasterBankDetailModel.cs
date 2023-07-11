using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Models
{
    public class CompanyMasterBankDetailModel
    {
        public int Id { get; set; }

        public int CompanyMasterId { get; set; }

        public string? BankName { get; set; }

        public string? BankAccountName { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? Ibannumber { get; set; }

        public string? BankIfsc { get; set; }

        public int IsActive { get; set; }
    }
}
