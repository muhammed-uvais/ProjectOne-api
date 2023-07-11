using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyMasterController : ControllerBase
    {
        private readonly ICompanyMasterService _companyMasterService;
        public CompanyMasterController(ICompanyMasterService companyMasterService)
        {
            this._companyMasterService = companyMasterService;
        }
        [HttpPost]
        [Route("CreateDefaultCompany")]
        public CommandResult CreateDefaultCompany(CompanyMasterModel model)
        {

            var k =  this._companyMasterService.CreateDefaultCompany(model);
            return k;
        }

        [HttpGet]
        [Route("GetDefaultCompany")]
        public CompanyMasterModel GetDefaultCompany()
        {
            return this._companyMasterService.GetDefaultCompany();
        }
    }
}
