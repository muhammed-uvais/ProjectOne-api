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
    public interface ITaxGroupService {

        CommandResult Create(TaxGroupModel model);
        IEnumerable<TaxGroupModel> GetAll();
        TaxGroupModel GetById(int id);
        CommandResult DeleteByID(int id);
    }



    public class TaxGroupService : ITaxGroupService
    {
        private readonly IMapper _mapper;
        private readonly ITaxGroupRepository _taxGroupRepository;
        public TaxGroupService(IMapper mapper, ITaxGroupRepository taxGroupRepository)
        {
           this._mapper = mapper;
            this._taxGroupRepository = taxGroupRepository;
        }
        public CommandResult Create(TaxGroupModel model)
        {
            var rtnval = _taxGroupRepository.Create(_mapper.Map<TaxGroupEntity>(model));
            
            return rtnval;
        }
        public IEnumerable<TaxGroupModel> GetAll()
        {
            var getroledata = _taxGroupRepository.GetAllTaxGroup()
                .OrderBy(e => e.Name);
            return _mapper.Map<IEnumerable<TaxGroupModel>>(getroledata);
        }
        public TaxGroupModel GetById(int id)
        {
            var getapprovaldata = _taxGroupRepository.GetTaxGroupById(id);
            getapprovaldata.TaxGroupChildsList = _taxGroupRepository
                .GetTaxGroupChildsEntityrByHdrId(getapprovaldata.Id);
            return _mapper.Map<TaxGroupModel>(getapprovaldata);
        }
        public CommandResult DeleteByID(int id)
        {
            var deletedData = _taxGroupRepository.DeleteByID(id); return deletedData;
        }
    }
}
