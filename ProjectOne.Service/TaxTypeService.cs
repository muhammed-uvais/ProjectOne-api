using AutoMapper;
using ProjectOne.Common;
using ProjectOne.Data.DbEntities;
using ProjectOne.Models;
using ProjectOne.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Service
{
    public interface ITaxTypeService 
    {
        IEnumerable<TaxTypeModel> GetAll();
        TaxTypeModel GetById(int id);
        CommandResult Delete(IEnumerable<TaxTypeModel> model);
    }
    public class TaxTypeService : ITaxTypeService
    {
        private readonly IMapper _mapper;
        private readonly ITaxTypeRepository _taxTypeRepository;
        public TaxTypeService(ITaxTypeRepository taxTypeRepository,
           IMapper mapper)
        {
            this._taxTypeRepository = taxTypeRepository;
            this._mapper = mapper;
        }
        public IEnumerable<TaxTypeModel> GetAll()
        {
            var getroledata = _taxTypeRepository.GetAllTaxType()
                .OrderBy(e => e.TaxType1);
            return _mapper.Map<IEnumerable<TaxTypeModel>>(getroledata);
        }
        public TaxTypeModel GetById(int id)
        {
            var getapprovaldata = _taxTypeRepository.GetTaxTypeById(id);
            return _mapper.Map<TaxTypeModel>(getapprovaldata);
        }
        public CommandResult Delete(IEnumerable<TaxTypeModel> model)
        {
            return _taxTypeRepository
                .Delete(_mapper.Map<IEnumerable<TaxType>>(model));
        }
    }
}
