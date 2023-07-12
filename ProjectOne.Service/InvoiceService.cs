using AutoMapper;
using ProjectOne.Common;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;
using ProjectOne.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Service
{
    public interface IInvoiceService {
        CommandResult CreateInvoice(InvoiceHdrModel model);
        CommandResult DeleteById(List<int> IDs);
        List<InvoiceHdrModel> GetAll();
        InvoiceHdrModel GetById(int Id);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
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
        public List<InvoiceHdrModel> GetAll()
        {
            // var Rtndata = _invoiceRepository.GetAll();
            //return _mapper.Map<List<InvoiceHdrModel>>(Rtndata);
            return _mapper.Map<List<InvoiceHdrModel>>(_invoiceRepository.GetAll());
            
        }
        public InvoiceHdrModel GetById(int Id)
        {
            return _mapper.Map<InvoiceHdrModel>(_invoiceRepository.GetById(Id));
        }
    }
}
