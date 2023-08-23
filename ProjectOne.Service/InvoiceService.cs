using AutoMapper;
using Microsoft.Extensions.Primitives;
using ProjectOne.Common;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;
using ProjectOne.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectOne.Service
{
    public interface IInvoiceService {
        CommandResult CreateInvoice(InvoiceHdrModel model);
        CommandResult DeleteById(List<int> IDs);
        List<InvoiceHdrModel> GetAll(DateTime? FromDate, DateTime? ToDate);
        InvoiceHdrModel GetById(int Id);
        string GeneratePDF(int Inv);

        List<InvoiceCustomerDetailModel> SearchCustomerByName(string customerName);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICompanyMasterRepository _companyMasterRepository;

        public InvoiceService(IMapper mapper, IInvoiceRepository invoiceRepository, ICompanyMasterRepository companyMasterRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
            _companyMasterRepository = companyMasterRepository;
        }
        public CommandResult CreateInvoice(InvoiceHdrModel model)
        {
            return _invoiceRepository.CreateInvoice(_mapper.Map<InvoiceHdrEntity>(model));

        }
        public CommandResult DeleteById(List<int> IDs)
        {
            CommandResult rslt = new CommandResult();
            List<string> lsterror = new List<string>();
            foreach (int id in IDs)
            {
                _invoiceRepository.DeleteById(id);
            }
            rslt.ErrorCodes = lsterror;
            return rslt;
        }
        public List<InvoiceHdrModel> GetAll(DateTime? FromDate, DateTime? ToDate)
        {
            // var Rtndata = _invoiceRepository.GetAll();
            //return _mapper.Map<List<InvoiceHdrModel>>(Rtndata);
            return _mapper.Map<List<InvoiceHdrModel>>(_invoiceRepository.GetAll(FromDate ,ToDate));
            
        }
        public InvoiceHdrModel GetById(int Id)
        {
            return _mapper.Map<InvoiceHdrModel>(_invoiceRepository.GetById(Id));
        }
       

        public string GeneratePDF(int Inv)
        {
            var getInvoicebyId = _invoiceRepository.GetById(Inv);
            var getDefaultCompany = _companyMasterRepository.GetDefaultCompany();

            var CompanyBankDetails = getDefaultCompany.CompanyMasterBankDetails.ToList()[0];
            var CompanyVatNumber = getDefaultCompany.VatNumber ?? "";

            var BanckAccountName = CompanyBankDetails.BankAccountName ?? "";
            var BankAccountNumber = CompanyBankDetails.BankAccountNumber ?? "";
            var BankIFSC = CompanyBankDetails.BankIfsc ?? "";
            var BankName = CompanyBankDetails.BankName ?? "";
            var Bankiban = CompanyBankDetails.Ibannumber ?? "";


            var NumberDisplay = getInvoicebyId.NumberDisplay;
            var EntryDate = getInvoicebyId.EntryDate.ToString("dd-MM-yyyy");

            var CustomerName = getInvoicebyId.CustomerDetails.Name ?? "";
            var CustomerAddress = getInvoicebyId.CustomerDetails.Address ?? "";
            var CustomerVAT = getInvoicebyId.CustomerDetails.Vatumber ?? "";
            var CustomerPhone = getInvoicebyId.CustomerDetails.Phone ?? "";
            var CustomerEmail = getInvoicebyId.CustomerDetails.Email ?? "";



            var imgfoldername = Path.Combine(Directory.GetCurrentDirectory(), "CompanyFiles");
            var logoimg = Path.Combine(imgfoldername, "companylogo.png");
            var textenglish = Path.Combine(imgfoldername, "textenglish.png");

            StringBuilder sb = new StringBuilder();
            sb.Append(
                "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "<title>Invoice</title>" +
                "</head>"

             );

            sb.Append(
             //   "<body style=\"font-family: Arial, sans-serif; line-height: 1.6; width: 21cm; height: 29.7cm; margin: 0 auto;\">" 
                "<body style=\"font-family: Arial, sans-serif; line-height: 1.6;margin: 0 auto;\">"

                );
            //headder
            
            sb.Append(
                "<div style=\"padding:0px 80px 20px 80px;\">"
                );

            sb.Append("<img src=\"" + logoimg + "\" alt=\"My Image\" style=\"height: 118px;width: 157px;object-fit: contain;\">");
            sb.Append("<img src=\"" + textenglish + "\" alt=\"My Image\" style=\"height: 110px;\">");
            sb.Append("</div>");
            sb.Append("<div>");
            sb.Append("<table style=\"border-collapse: collapse; width: 100%;\">" +
                " <tr>" +
                "<td><h2>Customer Details</h2></td>" +
                "<td> <h2>Invoice</h2></td>" +
                " </tr>" +
                "<tr>" +
                "<td style=\"word-break: break-all;\" >" +
                "<p style=\"margin: 0px 87px 5px 0px;\"><strong>Customer Name : </strong> " + CustomerName + "</p>");
                //"<p style=\"margin: 0px 87px 5px 0px;\"><strong>Address:</strong>" + CustomerAddress + "</p>" +


                if (CustomerVAT != "") {

                   sb.Append(  "<p style=\"margin: 0px 87px 5px 0px;\"><strong>TRN : </strong>" + CustomerVAT + "</p>"); 
                }


            sb.Append(

              //  "<p style=\"margin: 0px 87px 5px 0px;\"><strong>Email : </strong>" + CustomerEmail + "</p>" +
              //"<p style=\"margin: 0px 87px 5px 0px;\"><strong>Phone : </strong>" + CustomerPhone + "</p>" +

              "</td>");
            sb.Append(

                "<td  style=\"word-break: break-all;\">" +
                "<p style=\"margin: 0px 87px 5px 0px;\"><strong>Invoice Number : </strong>" + NumberDisplay + "</p>" +
                "<p style=\"margin: 0px 87px 5px 0px;\"><strong>Invoice Date : </strong>" + EntryDate + "</p>");
            if(getInvoicebyId.DisableTrn == 0)
            {

              sb.Append(
                "<p style=\"margin: 0px 87px 5px 0px;\"><strong>TRN : </strong>" + CompanyVatNumber + "</p>" +
                "</td>"); 
            }

            sb.Append(
                "</tr>" +
                "<tr>" +
                "<td style=\"word-break: break-all;width: 60%;\" >" +
                "<p style=\"margin: 0px 87px 5px 0px;\"><strong>Address : </strong>"+ CustomerAddress + "</p>" +
                "</td>" +
                "<td style=\"width: 40%;\" >" +
                "</td>" +
                "</tr>" +
                " </table>" +
              //  "<hr>" +
                "");
            sb.Append("</div>");
            sb.Append("<div style=\"padding: 20px 5px 10px 5px;\">" +
                "<table style=\"width: 100%; border-collapse: collapse;border: 1px solid black; \">" +
                "<tr>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">No</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">Description</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;white-space:normal;\">Date</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">Qty/Day</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">Price</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">VAT %</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">Taxable Value (AED)</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">VAT (AED)</th>" +
                "<th style=\"border: 1px solid black; padding: 8px; text-align: center;\">Total Inc.</th>" +
                "</tr>");


            var slno = 1;

            foreach (var item in getInvoicebyId.InvoiceItems)
            {


                sb.Append("<tr>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">" + slno++ +"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: left;border-right: 1px solid black;\">" + item.Description+"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;white-space:nowrap;border-right: 1px solid black;\">" + item.Date.ToString().Split(" ")[0] + "</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">" + item.QtyPerDay+"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">" + item.Price +"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">"  + item.Vatpercentage +"</td>" +
                    "<td style=\"padding:20px 8px; text-align: center;border-right: 1px solid black;\">" + item.TaxableValue+"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">" + item.Vatamount +"</td>" +
                    "<td style=\"padding: 20px 8px; text-align: center;border-right: 1px solid black;\">" + item.TotalAmount +"</td>" +
                    "</tr>");

            }
            sb.Append("</table>" + "</div>");

            var taxableval = getInvoicebyId.InvoiceAmount.TaxableValue.ToString() ?? "";
            var vatamt = getInvoicebyId.InvoiceAmount.Vatamount.ToString() ?? "";
            var totalamt = getInvoicebyId.InvoiceAmount.TotalAmount.ToString() ?? "";

            sb.Append("<div style=\"margin-bottom: 20px;margin-left: 500px;\">" +
                "<table>" +

                "<tr>" +
                "<td style=\"text-align: right; \"><strong>Taxable Value (AED):</strong></td>" +
                "<td style=\"text-align: right;padding-left:25px;\">" + taxableval + "</td>" +
                "</tr>" +

                "<tr>"+
                 "<td style=\"text-align: right; \"><strong>VAT @ 5%:</strong></td>" +
                "<td style=\"text-align: right;padding-left:25px;\">" + vatamt + "</td>" +
                "</tr>" +

                "<tr>" +
                "<td style=\"text-align: right; \"><strong>Total Amount in AED (AED):</strong></td>" +
                "<td style=\"text-align: right;padding-left:25px;\">" + totalamt + "</td>" +
                "</tr>" +
                "</table>" +
                "</div>" +
                "" +
                "" +
                "");


            sb.Append("<div style=\"padding: 10px 4px;\">" +
                "<table style=\"border-collapse: collapse;border: 1px solid black;\">" +
                "<tr style=\"border: 1px solid black;\">" +
                "<th colspan=\"2\" style=\"text-align: center;\">Bank Details</th>" +
                "</tr>" +

                "<tr> <td style=\"padding:2px 0px 5px 5px;width:100px;text-align:left;\"> <strong>Name</strong>  </td><td style=\"width:300px;text-align:left;\"> :   " + BanckAccountName + "</td></tr>" +
                "<tr> <td style=\"padding:2px 0px 5px 5px;width:100px;\"> <strong>Bank</strong>  </td><td style=\"width:300px;text-align:left;\"> :   " + BankName + "</td></tr>" +
                "<tr> <td style=\"padding:2px 0px 5px 5px;width:100px;\"> <strong>A/C No.</strong> </td><td style=\"width:300px;text-align:left;\"> :   " + BankAccountNumber + "</td></tr>" +
                "<tr> <td style=\"padding:2px 0px 5px 5px;width:100px;\"><strong>IBAN No.</strong>  </td><td style=\"width:300px;text-align:left;\"> :   " + Bankiban + "</td></tr>" +
                "<tr> <td style=\"padding:2px 0px 5px 5px;width:100px;\"><strong>IFSC Code</strong>  </td><td style=\"width:300px;text-align:left;\"> :   " + BankIFSC + "</td> </tr>" +
                "</table>" +
                "</div>");

            sb.Append("</body>");
            sb.Append("</html>");

                return sb.ToString();
        }
        public List<InvoiceCustomerDetailModel> SearchCustomerByName(string customerName)
        {
            return _mapper.Map<List<InvoiceCustomerDetailModel>>(_invoiceRepository.SearchCustomerByName(customerName));
        }
    }
}
