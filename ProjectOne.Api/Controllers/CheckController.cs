using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly IClass1Service _class1Service;
        public CheckController(IClass1Service class1Service) {
            this._class1Service = class1Service;
        }

        [Route("check")]
        [HttpGet]
        public string GetName()
        {
            return this._class1Service.NameData();
        }


    }
}
