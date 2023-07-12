using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService) {
            this._invoiceService = invoiceService;
        }
        [HttpPost]
        [Route("CreateInvoice")]
        public CommandResult CreateInvoice([FromBody]InvoiceHdrModel model)
        {
            return _invoiceService.CreateInvoice(model);
        }
        [HttpPost]
        [Route("DeleteInvoice")]
        public CommandResult DeleteInvoice([FromBody] List<int> Ids)
        {
            return _invoiceService.DeleteById(Ids);
        }
        [HttpGet]
        [Route("GetAll")]
        public List<InvoiceHdrModel> GetAll()
        {
           return _invoiceService.GetAll();
        }
        [HttpGet]
        [Route("GetById")]
        public InvoiceHdrModel GetById(int id)
        {
            return _invoiceService.GetById(id);
        }
    }
}
