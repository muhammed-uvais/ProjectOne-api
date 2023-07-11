using ProjectOne.Common;
using ProjectOne.Data.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOne.Repository.Repository
{
    public interface ITaxTypeRepository 
    {
        TaxType GetTaxTypeById(int id);
        IEnumerable<TaxType> GetAllTaxType();
        CommandResult Delete(IEnumerable<TaxType> entity);
    }
    public class TaxTypeRepository : ITaxTypeRepository
    {
        private readonly ProjectOneContext Context;
        private readonly CommandResult Rslt;
        public TaxTypeRepository(ProjectOneContext context, CommandResult rslt)
        {
            this.Context = context;
            Rslt = rslt;
        }
        public CommandResult DeleteById(int Id)
        {
            IEnumerable<string> errorstr = new string[] { };

            var getdata = Context.TaxTypes.Find(Id);
            getdata.IsActive = 0;
            Context.SaveChanges();

            Rslt.ErrorCodes = errorstr;
            return Rslt;
        }
        public CommandResult Delete(IEnumerable<TaxType> entity)
        {
            IEnumerable<string> errorstr = new string[] { };
            string[] arrerror = new string[1];

            entity.ToList().ForEach(element => DeleteById(element.Id));

            Rslt.ErrorCodes = errorstr;
            return Rslt;
        }
        public IEnumerable<TaxType> GetAllTaxType()
        {
            var entities = from hdr in Context.TaxTypes
                           where hdr.IsActive == 1
                           select new TaxType()
                           {
                               Id = hdr.Id,
                               TaxType1 = hdr.TaxType1,
                               IsEditable = hdr.IsEditable,
                               IsActive = hdr.IsActive
                           };
            return entities;
        }
        public TaxType GetTaxTypeById(int id)
        {
            var entity = Context.TaxTypes
                .Where(e => e.IsActive == 1
                    && e.Id == id)
                .Select(hdr => new TaxType
                {
                    Id = hdr.Id,
                    TaxType1 = hdr.TaxType1,
                    IsEditable = hdr.IsEditable,
                    CompanyMasterId = hdr.CompanyMasterId,
                    BranchMasterId = hdr.BranchMasterId,
                    CreatedBy = hdr.CreatedBy,
                    CreatedDate = hdr.CreatedDate,
                    ModifiedBy = hdr.ModifiedBy,
                    ModifiedDate = hdr.ModifiedDate,
                    IsActive = hdr.IsActive
                })
                .FirstOrDefault();
            return entity;
        }
    }
}
