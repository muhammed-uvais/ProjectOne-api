using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxGroupController : ControllerBase
    {
        private readonly ITaxGroupService _taxGroupService;
        public TaxGroupController(ITaxGroupService taxGroupService)
        {
            _taxGroupService = taxGroupService;
        }
        [HttpGet]
        [Route("GetTaxGroup")]
        public IEnumerable<TaxGroupModel> GetTaxGroup()
        {
            return this._taxGroupService.GetAll();
        }

        [HttpGet]
        [Route("GetTaxGroupById")]
        public TaxGroupModel GetTaxGroupById(int id)
        {
            return this._taxGroupService.GetById(id);
        }
        [HttpPost]
        [Route("CreateTaxGroup")]
        public CommandResult TaxGroupCrud([FromBody] TaxGroupModel model)
        {
            return _taxGroupService.Create(model);
        }
        [HttpGet]
        [Route("DeleteByID")]
        public CommandResult DeleteByID(int id)
        {
            return _taxGroupService.DeleteByID(id);
        }
    }
}
