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
        List<InvoiceHdrEntity> GetAll();
        InvoiceHdrEntity GetById(int Id);
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
                        refnodisplay = prefix + (refno + 1).ToString();
                    }

                    InvoiceHdr invoicehdr = new InvoiceHdr
                    {
                        Number = refno,
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
                    Rslt.Id = invoicehdr.Id;
                }
                else
                {
                    var existinginvoice = (from hdr in Context.InvoiceHdrs
                                           where hdr.NumberDisplay.ToUpper().Equals(entity.NumberDisplay.ToUpper())
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
                    InvoiceHdr invoiceHdr = Context.InvoiceHdrs.Find(entity.Id);
                    if (invoiceHdr != null)
                    {
                        invoiceHdr.EntryDate = entity.EntryDate;
                        invoiceHdr.CreatedDate =DateTime.Now;
                        Context.SaveChanges();
                    }
                    var previousItems = Context.InvoiceContents.Where(x => x.InvoiceHdrId == entity.Id).ToList();
                    previousItems.ForEach(x => { x.IsActive = 0; });
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
        public List<InvoiceHdrEntity> GetAll()
        {
            var rtndata = (from hdr in Context.InvoiceHdrs
                           where hdr.IsActive == 1
                           select new InvoiceHdrEntity
                           {
                               Id = hdr.Id,
                               EntryDate    = hdr.EntryDate,
                               Number   = hdr.Number,
                               NumberDisplay =  hdr.NumberDisplay,
                               CreatedDate = hdr.CreatedDate,
                               IsActive = hdr.IsActive
                           }).ToList();
            return rtndata;
        }
       public InvoiceHdrEntity GetById(int Id)
       {
            var rtndata = (from hdr in Context.InvoiceHdrs
                           where hdr.IsActive == 1 && hdr.Id == Id
                           select new InvoiceHdrEntity{
                               Id = hdr.Id,
                               EntryDate = hdr.EntryDate,
                               Number = hdr.Number,
                               NumberDisplay = hdr.NumberDisplay,
                               CreatedDate = hdr.CreatedDate,
                               IsActive = hdr.IsActive,
                           }).FirstOrDefault();
             if (rtndata != null)
             {
                rtndata.InvoiceItems = (from chld in Context.InvoiceContents
                                        where chld.IsActive == 1 &&
                                        chld.Id == rtndata.Id
                                        select new InvoiceContentEntity
                                        {
                                            Id = chld.Id,
                                            InvoiceHdrId =chld.InvoiceHdrId,
                                            Description = chld.Description,
                                            Date    = chld.Date,
                                            QtyPerDay = chld.QtyPerDay,
                                            Vatpercentage = chld.Vatpercentage,
                                            TaxableValue = chld.TaxableValue,
                                            Vatamount = chld.Vatamount,
                                            TotalAmount = chld.TotalAmount,
                                            IsActive = chld.IsActive,
                                        }).ToList();
             }
            return rtndata;
        }

    }
}
