using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectOne.Common
{
    public class CommandResult
    {
  

        public long? Id { get; set; }

        public IEnumerable<string> ErrorCodes { get; set; }
        public IEnumerable<string> Others { get; set; }

        public bool IsSuccess
        {
            get
            {
                return !ErrorCodes.Any();
            }
        }
    }
}
