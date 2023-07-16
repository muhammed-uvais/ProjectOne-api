using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            this._invoiceService = invoiceService;
        }
        [HttpPost]
        [Route("CreateInvoice")]
        public CommandResult CreateInvoice([FromBody] InvoiceHdrModel model)
        {
            var kkk = model;
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
        public List<InvoiceHdrModel> GetAll(DateTime? FromDate, DateTime? ToDate)
        {
            return _invoiceService.GetAll(FromDate, ToDate);
        }
        [HttpGet]
        [Route("GetById")]
        public InvoiceHdrModel GetById(int id)
        {
            return _invoiceService.GetById(id);
        }

        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF(int InvoiceNo)
        {
            var pdf = new PdfDocument();
            StringBuilder htmlcontent = new StringBuilder();

            htmlcontent.Append(
                "<h1>hiiiii</hi>"
                );
            PdfGenerator.AddPdfPages(pdf, htmlcontent.ToString(), PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms);
                response = ms.ToArray();
            }
            string Filename = "Invoice_" + InvoiceNo + ".pdf";
            return File(response, "application/pdf", Filename);

        }
    }
}
