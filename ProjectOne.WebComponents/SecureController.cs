using Microsoft.AspNetCore.Authorization;
using ProjectOne.WebComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.WebComponents
{
    [Authorize]
    public class SecureController : BaseController
    {
    }
}
