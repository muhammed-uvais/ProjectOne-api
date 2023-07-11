using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Service;
using ProjectOne.WebComponents;
using System.Net.Http.Headers;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : SecureController
    {
        private readonly IUserMasterService _userMasterService;
        public UserMasterController(IUserMasterService usermasterservice) {
        this._userMasterService = usermasterservice;
        }
        [HttpGet]
        [Route("GetById")]
        public string GetUserById(long Id)
        {
            return _userMasterService.getUserNameById(Id);
        }
    }
}
