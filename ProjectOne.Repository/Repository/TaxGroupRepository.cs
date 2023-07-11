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
    public interface ITaxGroupRepository {
        CommandResult Create(TaxGroupEntity entity);
        TaxGroupEntity GetTaxGroupById(int id);
        IEnumerable<TaxGroupEntity> GetAllTaxGroup();
        IEnumerable<TaxGroupChildsEntity> GetTaxGroupChildsEntityrByHdrId(int HdrId);
        CommandResult DeleteByID(int id);
    }

    public class TaxGroupRepository  : ITaxGroupRepository
    {
        private readonly ProjectOneContext Context;
        private readonly CommandResult Rslt;
        public TaxGroupRepository(ProjectOneContext context, CommandResult rslt) { Context = context; Rslt = rslt; }
        public CommandResult Create(TaxGroupEntity entity)
        {
            var transaction = Context.Database.BeginTransaction();
            IEnumerable<string> errorstr = new string[] { };
            List<string> lsterror = new List<string>();
            if (entity.Id.Equals(0))
            {
                var getexistingdata = Context.TaxGroups
                    .Where(e => e.IsActive == 1
                        && e.Name.Trim().ToUpper().Equals(entity.Name.Trim().ToUpper()))
                    .FirstOrDefault();

                if (getexistingdata != null)
                {
                    lsterror.Add("");
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





                TaxGroup hdrobj = new TaxGroup
                {
                    Name = entity.Name,
                    TaxTypeId = entity.TaxTypeId,
                    CompanyMasterId = entity.CompanyMasterId,
                    BranchMasterId = entity.BranchMasterId,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                    IsEditable = 1, //entity.IsEditable,
                    IsActive = 1
                };
                Context.TaxGroups.Add(hdrobj);
                Context.SaveChanges();

                CreateTaxGroupChilds(entity.TaxGroupChildsList, hdrobj.Id);

                transaction.Commit();
                Rslt.Id = hdrobj.Id;
            }
            else
            {
                var getexistinghdrs = Context.TaxGroups.FirstOrDefault(x => x.Id == entity.Id);
                if (getexistinghdrs != null)
                {
                    var getexistingdata = Context.TaxGroups
                        .Where(e => e.IsActive == 1
                            && e.Name.Trim().ToUpper().Equals(entity.Name.Trim().ToUpper())
                            && e.Id != getexistinghdrs.Id)
                        .FirstOrDefault();

                    if (getexistingdata != null)
                    {
                        lsterror.Add(""); ;
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





                    getexistinghdrs.Name = entity.Name;
                    getexistinghdrs.TaxTypeId = entity.TaxTypeId;
                    getexistinghdrs.ModifiedBy = entity.ModifiedBy;
                    getexistinghdrs.ModifiedDate = entity.ModifiedDate;
                    getexistinghdrs.IsActive = 1;
                    Context.SaveChanges();

                    CreateTaxGroupChilds(entity.TaxGroupChildsList, getexistinghdrs.Id);

                    transaction.Commit();
                    Rslt.Id = getexistinghdrs.Id;
                }
            }
            Rslt.ErrorCodes = lsterror;
            return Rslt;
        }

        public void CreateTaxGroupChilds(
            IEnumerable<TaxGroupChildsEntity> TaxGroupChildsList, int HdrId)
        {
            var getval = Context.TaxGroupChilds
                .Where(e => e.TaxGroupId == HdrId && e.IsActive == 1)
                .ToList();
            if (getval.Count > 0)
            {
                getval.ForEach(e => e.IsActive = 0);
                Context.SaveChanges();
            }

            foreach (var item in TaxGroupChildsList)
            {
                TaxGroupChild obj = new TaxGroupChild
                {
                    TaxGroupId = HdrId,
                    Name = item.Name,
                    Rate = item.Rate,
                    IsActive = item.IsActive,
                };
                Context.TaxGroupChilds.Add(obj);
                Context.SaveChanges();
                item.Id = obj.Id;
            }
        }
        public IEnumerable<TaxGroupEntity> GetAllTaxGroup()
        {
            
            {
                var entities = (from hdr in Context.TaxGroups
                                    //join type in Context.TaxType
                                    // on hdr.TaxTypeId equals type.Id
                                where hdr.IsActive == 1
                                //&& type.IsActive == 1
                                select new TaxGroupEntity()
                                {
                                    Id = hdr.Id,
                                    Name = hdr.Name,
                                    TaxTypeId = hdr.TaxTypeId,
                                    //TaxTypeName = type.TaxType1,
                                    IsEditable = hdr.IsEditable,
                                    IsActive = hdr.IsActive,
                                    CreatedDate = hdr.CreatedDate
                                }).ToList();
                foreach (var item in entities)
                {
                    item.TaxGroupChildsList = GetTaxGroupChildsEntityrByHdrId(item.Id).ToList();
                }
                return entities;
            }

        }

        public TaxGroupEntity GetTaxGroupById(int id)
        {
            var entity = Context.TaxGroups
                .Where(e => e.IsActive == 1
                    && e.Id == id)
                .Select(hdr => new TaxGroupEntity
                {
                    Id = hdr.Id,
                    Name = hdr.Name,
                    TaxTypeId = hdr.TaxTypeId,
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

        public IEnumerable<TaxGroupChildsEntity> GetTaxGroupChildsEntityrByHdrId(int HdrId)
        {
            var entity = Context.TaxGroupChilds
                .Where(e => e.TaxGroupId == HdrId
                    && e.IsActive == 1)
                .Select(hdr => new TaxGroupChildsEntity
                {
                    Id = hdr.Id,
                    Name = hdr.Name,
                    TaxGroupId = hdr.TaxGroupId,
                    Rate = hdr.Rate,
                    IsActive = hdr.IsActive
                });
            return entity;
        }
        public CommandResult DeleteByID(int id)
        {
            var transaction = Context.Database.BeginTransaction();
            IEnumerable<string> errorstr = new string[] { };
            List<string> lsterror = new List<string>();
            var Deleted = Context.TaxGroups.Where(x => x.Id == id).FirstOrDefault();
            Deleted.IsActive = 0;
            Context.SaveChanges();
            transaction.Commit();
            Rslt.Id = Deleted.Id;
            Rslt.ErrorCodes = lsterror;
            return Rslt;
        }

    }
}
