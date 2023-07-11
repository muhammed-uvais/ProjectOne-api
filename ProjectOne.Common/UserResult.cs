using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Common
{
    public class UserResult
    {
        public long LoginId { get; set; }
        public string LoginName { get; set; }
        public long EmployeeId { get; set; }
        public int LoginStatus { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public int? IsAdmin { get; set; }
        public IEnumerable<string> ErrorCodes { get; set; }
        public bool IsSuccess
        {
            get
            {
                return !ErrorCodes.Any();
            }
        }
        public string Token { get; set; }
    }
}
