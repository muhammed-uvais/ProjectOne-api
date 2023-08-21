using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using ProjectOne.Common;
using ProjectOne.Models;
using ProjectOne.Service;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Wkhtmltopdf.NetCore;

namespace ProjectOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        readonly IGeneratePdf _generatePdf;
        readonly IConverter _converter;
        public InvoiceController(IInvoiceService invoiceService, IGeneratePdf generatePdf,
            IConverter converter)
        {
            this._invoiceService = invoiceService;
            _generatePdf = generatePdf;
            _converter = converter;
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

        [HttpGet("generatepdf1")]
        public async Task<IActionResult> GeneratePDF(int InvoiceNo)
        {
            var pdf = new PdfDocument();

            var htmlcontent =  _invoiceService.GeneratePDF(InvoiceNo);

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
       
        [HttpGet("generatepdf")]
        public async Task<IActionResult> PDF(int InvoiceNo)
        {
            var objnew = new JObject();

            var htmlcontent = _invoiceService.GeneratePDF(InvoiceNo);

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "DownloadFiles");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filename = Guid.NewGuid();
            var filePath = Path.Combine($"{folderPath}/" + "Invoice" + ".Pdf");

            string[] files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings() { Top = 10,Bottom = 10 },
                        Out = filePath,
                    },
                Objects = {
                        new ObjectSettings() {
                            PagesCount = true,
                            WebSettings = { DefaultEncoding = "utf-8" },
                           // HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                             //FooterSettings  = { FontName = "Arial", FontSize = 9,Left= "Checked by:",Center= "Approved by:",Right= "Received by:",Spacing = 3.5},
                             //FooterSettings  = { FontName = "Arial", FontSize = 7,Right= "Printed By : "+printedbyname.ToString()+"",Center="Printed Date & Time : "+DateTime.Now.ToString("dd-MM-yyyy")+ "   " + DateTime.Now.ToString("HH:mm") + "", Left= "Prepared By  :" + preparedbyname.ToString() + "", Spacing =3.5},
                             HtmlContent = htmlcontent.ToString(),
                        }
                    }
            };
            
            _converter.Convert(doc);

            var imgfoldername = Path.Combine(Directory.GetCurrentDirectory(), "DownloadFiles");
            var filetortn = Path.Combine(imgfoldername, "Invoice.pdf");
           // if (System.IO.File.Exists(filePath))
            {
                // Read the file bytes from the specified path
                byte[] fileBytes = System.IO.File.ReadAllBytes(filetortn);

                // Send the file as a response to the user
                return File(fileBytes, "application/pdf", "Invoice.pdf");
           

            }

            

           
        }
        [HttpGet]
        [Route("CustomerSearchByName")]
        public List<InvoiceCustomerDetailModel> CustomerSearchByName(string customerName)
        {
            return _invoiceService.SearchCustomerByName(customerName);
        }
    }
}
