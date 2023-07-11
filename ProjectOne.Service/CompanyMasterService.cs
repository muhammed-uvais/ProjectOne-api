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
    public interface ICompanyMasterService {
        CommandResult CreateDefaultCompany(CompanyMasterModel model);
        public CompanyMasterModel GetDefaultCompany();
    }
    public class CompanyMasterService : ICompanyMasterService
    {
        private readonly IMapper _mapper;
        private readonly ICompanyMasterRepository _companyMasterRepository;
        public CompanyMasterService(IMapper mapper,ICompanyMasterRepository companyMasterRepository)
        {
            _mapper = mapper;
            _companyMasterRepository = companyMasterRepository;
        }
        public CommandResult CreateDefaultCompany(CompanyMasterModel model)
        {
           

            var retval = _companyMasterRepository.CreateDefaultCompany(_mapper.Map<CompanyMasterEntity>(model));
            if (retval.IsSuccess)
            {
               
            }
            return retval;
        }
        public CompanyMasterModel GetDefaultCompany()
        {
           // var AdmnDocId = _admnDocRepository.GetAdmnDocIdByCode("CMPNY");
            var getdata = _companyMasterRepository.GetDefaultCompany();
            if (getdata == null)
            {
                getdata = new CompanyMasterEntity();
            }
            
            //getdata.DocumentManagerHdr = _documentManagerRepository.GetDocumentManagerHdrByDetails(
            //            AdmnDocId, "CompanyMaster", getdata.Id);

            //if (getdata.DocumentManagerHdr != null)
            //{
            //    getdata.FileName = getdata.DocumentManagerHdr.DocumentmanagerChild.Name + "." + getdata.DocumentManagerHdr.DocumentmanagerChild.Extension;
            //    getdata.FileUserName = getdata.DocumentManagerHdr.DocumentmanagerChild.UploadFileName;
            //    getdata.IsDocumentSelected = 0;
            //}
            return _mapper.Map<CompanyMasterModel>(getdata);
        }

    }
}
