using ProjectOne.Common;
using ProjectOne.Data.DbEntities;
using ProjectOne.Data.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Repository.Repository
{
    public interface IInvoiceRepository {
        CommandResult CreateInvoice(InvoiceHdrEntity entity);
        CommandResult DeleteById(int Id);
        List<InvoiceHdrEntity> GetAll(DateTime? FromDate, DateTime? ToDate);
        InvoiceHdrEntity GetById(int Id);
        List<InvoiceCustomerDetailEntity> SearchCustomerByName(string customerName);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ProjectOneContext Context;
        private readonly CommandResult Rslt;

        public InvoiceRepository(ProjectOneContext context, CommandResult rslt) {
            Context = context;
            Rslt = rslt;
        }
        public CommandResult CreateInvoice(InvoiceHdrEntity entity)
        {
            var transaction = Context.Database.BeginTransaction();
            IEnumerable<string> errorstr = new string[] { };
            List<string> lsterror = new List<string>();

            try
            {

                if(entity.Id.Equals(0))
                {
                    //check existing number display
                    //.......
                    var existinginvoice = (from inv in Context.InvoiceHdrs
                                           where inv.IsActive == 1
                                           && inv.NumberDisplay.ToUpper().Equals(entity.NumberDisplay.ToUpper())
                                           select inv).FirstOrDefault();
                    if(existinginvoice != null)
                    {
                        lsterror.Add("Invoice " + existinginvoice.NumberDisplay + " Exist");
                    }

                    if (lsterror.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(lsterror[0]))
                        {
                            transaction.Rollback();
                            Rslt.ErrorCodes = lsterror;
                            return Rslt;
                        }
                    }

                    //generate NumberDisplay
                    long refno = 0;
                    string refnodisplay = "";
                    string prefix = "INV";
                    if(Context.InvoiceHdrs.Where(x => x.Number > 0 && x.NumberDisplay != null).Count() > 0)
                    {
                        refno = Context.InvoiceHdrs.Select(x => x.Number).Max();
                        refno = refno + 1;
                        refnodisplay = prefix + refno.ToString();

                    }
                    else
                    {
                        refno = refno + 1;
                        refnodisplay = prefix + (refno).ToString();
                    }




                        InvoiceCustomerDetail invoiceCustomerDetail = new InvoiceCustomerDetail
                        {


                            Name = entity.CustomerDetails.Name,
                            Address = entity.CustomerDetails.Address,
                            Vatumber = entity.CustomerDetails.Vatumber,
                            Phone = entity.CustomerDetails.Phone,
                            Email = entity.CustomerDetails.Email,
                            IsActive = 1,
                        };


                    if (entity.CustomerDetails.Id.Equals(0))
                    {
                        Context.InvoiceCustomerDetails.Add(invoiceCustomerDetail);
                        Context.SaveChanges();
                    }
                    InvoiceHdr invoicehdr = new InvoiceHdr
                    {
                        InvoiceCustomerDetailsId = entity.CustomerDetails.Id == 0 ? invoiceCustomerDetail.Id : entity.CustomerDetails.Id ,
                        Number = refno,
                        DisableTrn = entity.DisableTrn,
                        EntryDate = entity.EntryDate,
                        NumberDisplay = refnodisplay,
                        CreatedDate = DateTime.Now,
                        IsActive = 1
                    };
                    Context.InvoiceHdrs.Add(invoicehdr);
                    Context.SaveChanges();



                    foreach (var item in entity.InvoiceItems)
                    {
                        if(item != null)
                        {
                            InvoiceContent invoiceContent = new InvoiceContent { 
                            
                                InvoiceHdrId = invoicehdr.Id,
                                Description = item.Description,
                                Date = item.Date,
                                QtyPerDay = item.QtyPerDay,
                                Price = item.Price,
                                Vatpercentage = item.Vatpercentage,
                                TaxableValue = item.TaxableValue,
                                Vatamount = item.Vatamount,
                                TotalAmount = item.TotalAmount,
                                IsActive = 1 
                            };
                            Context.InvoiceContents.Add(invoiceContent);
                            Context.SaveChanges();
                        }
                    }
                    InvoiceAmount invoiceAmount = new InvoiceAmount
                    {
                        InvoiceHdrId = invoicehdr.Id,
                        TaxableValue = entity.InvoiceAmount.TaxableValue,
                        Vatamount = entity.InvoiceAmount.Vatamount,
                        TotalAmount = entity.InvoiceAmount.TotalAmount,
                        IsActive = entity.InvoiceAmount.IsActive,

                    };
                    Context.InvoiceAmounts.Add(invoiceAmount);
                    Context.SaveChanges();

                    transaction.Commit();
                    Rslt.Id = invoicehdr.Id;
                }
                else
                {
                    var existinginvoice = (from hdr in Context.InvoiceHdrs
                                           where hdr.NumberDisplay.ToUpper().Equals(entity.NumberDisplay.ToUpper()) && hdr.Id != entity.Id 
                                           select hdr).FirstOrDefault();
                    if (existinginvoice != null)
                    {
                        lsterror.Add("Invoice" + existinginvoice.NumberDisplay + " exist");
                    }
                    if (lsterror.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(lsterror[0]))
                        {
                            transaction.Rollback();
                            Rslt.ErrorCodes = lsterror;
                            return Rslt;
                        }
                    }
                    InvoiceCustomerDetail detail = Context.InvoiceCustomerDetails.
                        Where(x =>  x.Id == entity.CustomerDetails.Id).FirstOrDefault();
                    if (detail != null)
                    {
                        detail.Name = entity.CustomerDetails.Name;
                        detail.Address = entity.CustomerDetails.Address;
                        detail.Vatumber = entity.CustomerDetails.Vatumber;
                        detail.Phone = entity.CustomerDetails.Phone;
                        detail.Email = entity.CustomerDetails.Email;
                        Context.SaveChanges();
                    }

                    InvoiceHdr invoiceHdr = Context.InvoiceHdrs.Find(entity.Id);
                    if (invoiceHdr != null)
                    {
                        invoiceHdr.InvoiceCustomerDetailsId = entity.CustomerDetails.Id;
                        invoiceHdr.EntryDate = entity.EntryDate;
                        invoiceHdr.DisableTrn = entity.DisableTrn;
                        invoiceHdr.CreatedDate =DateTime.Now;
                        Context.SaveChanges();
                    }
                    
                    var previousItems = Context.InvoiceContents.
                        Where(x => x.InvoiceHdrId == entity.Id && x.IsActive == 1).ToList();
                    previousItems.ForEach(x => { x.IsActive = 0; });
                    Context.SaveChanges();

                    var invamount = Context.InvoiceAmounts.
                        Where(x => x.InvoiceHdrId == entity.Id && x.IsActive == 1).FirstOrDefault();
                    invamount.IsActive = 0;
                    Context.SaveChanges();

                    InvoiceAmount invoiceAmount = new InvoiceAmount
                    {
                        InvoiceHdrId = invoiceHdr.Id,
                        TaxableValue = entity.InvoiceAmount.TaxableValue,
                        Vatamount = entity.InvoiceAmount.Vatamount,
                        TotalAmount = entity.InvoiceAmount.TotalAmount,
                        IsActive = entity.InvoiceAmount.IsActive,

                    };
                    Context.InvoiceAmounts.Add(invoiceAmount);
                    Context.SaveChanges();
                    foreach (var item in entity.InvoiceItems)
                    {
                        if (item != null)
                        {
                            InvoiceContent invoiceContent = new InvoiceContent
                            {

                                InvoiceHdrId = entity.Id,
                                Description = item.Description,
                                Date = item.Date,
                                QtyPerDay = item.QtyPerDay,
                                Price = item.Price,
                                Vatpercentage = item.Vatpercentage,
                                TaxableValue = item.TaxableValue,
                                Vatamount = item.Vatamount,
                                TotalAmount = item.TotalAmount,
                                IsActive = 1
                            };
                            Context.InvoiceContents.Add(invoiceContent);
                            Context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                    Rslt.Id = entity.Id;
                }

                Rslt.ErrorCodes = lsterror;
                return Rslt;
            }
            catch(Exception ex) { }
            {
                transaction.Rollback();
                lsterror.Add("Exception Caused");
                Rslt.ErrorCodes = lsterror;
                return Rslt;

            }
        }
        public CommandResult DeleteById(int Id) {
            var transaction = Context.Database.BeginTransaction();
            IEnumerable<string> errorstr = new string[] { };
            List<string> lsterror = new List<string>();
            try {
            
                var Delete = Context.InvoiceHdrs.Where(x => x.Id == Id).FirstOrDefault();
                if (Delete != null)
                {
                    Delete.IsActive = 0;
                    Context.SaveChanges();
                }
                transaction.Commit();
                Rslt.ErrorCodes = lsterror;
                return Rslt;
            }
            catch(Exception ex) {
                transaction.Rollback();
                lsterror.Add("Exception Caused");
                Rslt.ErrorCodes = lsterror;
                return Rslt;
            }
        }
        public List<InvoiceHdrEntity> GetAll(DateTime? FromDate , DateTime? ToDate)
        {
            var rtndata = (from hdr in Context.InvoiceHdrs
                           join customer in Context.InvoiceCustomerDetails
                           on hdr.InvoiceCustomerDetailsId equals customer.Id
                           where hdr.IsActive == 1
                           && customer.IsActive == 1
                           select new InvoiceHdrEntity
                           {
                               Id = hdr.Id,
                               EntryDate = hdr.EntryDate,
                               Number = hdr.Number,
                               NumberDisplay = hdr.NumberDisplay,
                               CustomerName = customer.Name,
                               CreatedDate = hdr.CreatedDate,
                               IsActive = hdr.IsActive
                           }).OrderByDescending(f => f.Number).ToList();
            if(FromDate != null && ToDate != null )
            {
                rtndata = rtndata.Where(hdr =>  hdr.EntryDate.Date >=  FromDate.Value.Date && hdr.EntryDate.Date <= ToDate.Value.Date).ToList();
            }
            return rtndata;
        }
        public InvoiceHdrEntity GetById(int Id)
       {
            var rtndata = (from hdr in Context.InvoiceHdrs
                           where hdr.IsActive == 1 && hdr.Id == Id
                           select new InvoiceHdrEntity{
                               Id = hdr.Id,
                               InvoiceCustomerDetailsId = hdr.InvoiceCustomerDetailsId,
                               EntryDate = hdr.EntryDate,
                               DisableTrn = hdr.DisableTrn,
                               Number = hdr.Number,
                               NumberDisplay = hdr.NumberDisplay,
                               CreatedDate = hdr.CreatedDate,
                               IsActive = hdr.IsActive,
                           }).FirstOrDefault();
             if (rtndata != null)
             {
                rtndata.InvoiceItems = (from chld in Context.InvoiceContents
                                        where chld.IsActive == 1 &&
                                        chld.InvoiceHdrId == rtndata.Id
                                        select new InvoiceContentEntity
                                        {
                                            Id = chld.Id,
                                            InvoiceHdrId =chld.InvoiceHdrId,
                                            Description = chld.Description,
                                            Date    = chld.Date,
                                            QtyPerDay = chld.QtyPerDay,
                                            Price = chld.Price,
                                            Vatpercentage = chld.Vatpercentage,
                                            TaxableValue = chld.TaxableValue,
                                            Vatamount = chld.Vatamount,
                                            TotalAmount = chld.TotalAmount,
                                            IsActive = chld.IsActive,
                                        }).ToList();
                rtndata.CustomerDetails = (from customer in Context.InvoiceCustomerDetails
                                           where customer.Id == rtndata.InvoiceCustomerDetailsId && customer.IsActive == 1
                                           select new InvoiceCustomerDetailEntity
                                           {
                                               Id = customer.Id,
                                               Name = customer.Name,
                                               Address = customer.Address,
                                               Vatumber =customer.Vatumber,
                                               Phone = customer.Phone,
                                               Email = customer.Email,
                                               IsActive = customer.IsActive
                                           }).FirstOrDefault();
                rtndata.InvoiceAmount = (from amt in Context.InvoiceAmounts
                                         join inv in Context.InvoiceHdrs
                                         on amt.InvoiceHdrId equals inv.Id
                                         where amt.IsActive == 1
                                         && amt.InvoiceHdrId == rtndata.Id
                                         select new InvoiceAmountEntity {
                                            Id = amt.Id,
                                            InvoiceHdrId =amt.InvoiceHdrId,
                                            TaxableValue = amt.TaxableValue,    
                                            Vatamount = amt.Vatamount,
                                            TotalAmount = amt.TotalAmount,
                                            IsActive = amt.IsActive,
                                         }).FirstOrDefault();

                }
            return rtndata;
        }
        public List<InvoiceCustomerDetailEntity>SearchCustomerByName(string customerName)
        {
            if (customerName == null || customerName == "fetchall")
            {
                customerName = "";
            }

            var rtndata = (from customer in Context.InvoiceCustomerDetails
                           where customer.IsActive == 1
                           && customer.Name.ToLower().Contains(customerName.Trim().ToLower())
                           select new InvoiceCustomerDetailEntity {
                               Id=customer.Id,
                               Name = customer.Name,
                               Address = customer.Address,
                               Email = customer.Email,
                               Phone = customer.Phone,
                               Vatumber = customer.Vatumber
                               
                           
                           }).ToList();

            return rtndata;
        }

    }
}
