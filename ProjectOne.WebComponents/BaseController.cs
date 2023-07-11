
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProjectOne.WebComponents
{
    public interface IBaseController
    {
        ModelStateDictionary ModelState { get; }

        ControllerContext Context { get; }
    }
    public class BaseController : ControllerBase, IBaseController
    {
        public ControllerContext Context
        {
            get
            {
                return ControllerContext;
            }
        }

       
    }
}
