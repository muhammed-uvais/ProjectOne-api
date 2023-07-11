using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Common;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        public UserLoginController(IUserLoginService userLoginService) {
        this._userLoginService = userLoginService;
        }

        [HttpPost]
        [Route("CheckLogin")]
        public UserResult CheckLogin(UserLoginModel model)
        {
            return _userLoginService.checklogin(model);   
        }

    }
}
