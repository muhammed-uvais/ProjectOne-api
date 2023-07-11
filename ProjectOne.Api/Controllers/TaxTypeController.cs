using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxTypeController : ControllerBase
    {
        private readonly ITaxTypeService _taxTypeService;
        public TaxTypeController(ITaxTypeService taxTypeService)
        {
            this._taxTypeService = taxTypeService;
        }
        [HttpGet]
        [Route("GetTaxType")]
        public IEnumerable<TaxTypeModel> GetTaxType()
        {
            return this._taxTypeService.GetAll();
        }

        [HttpGet]
        [Route("GetTaxTypeById")]
        public TaxTypeModel GetTaxTypeById(int id)
        {
            return this._taxTypeService.GetById(id);
        }



        [HttpPost]
        [Route("DeleteTaxType")]
        public CommandResult DeleteTaxType([FromBody] TaxTypeModel[] model)
        {
            return _taxTypeService.Delete(model);
        }

    }
}
