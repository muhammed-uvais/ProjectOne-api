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
    public interface ICompanyMasterRepository {
        CommandResult CreateDefaultCompany(CompanyMasterEntity entity);
        CompanyMasterEntity GetDefaultCompany();
    }
    public class CompanyMasterRepository : ICompanyMasterRepository
    {
        private readonly ProjectOneContext Context;
        private readonly CommandResult Rslt;
        public CompanyMasterRepository(ProjectOneContext context, CommandResult rslt)
        {
            Context = context;
            Rslt = rslt;
        }
        public CommandResult CreateDefaultCompany(CompanyMasterEntity entity)
        {
            var transaction = Context.Database.BeginTransaction();
            IEnumerable<string> errorstr = new string[] { };
            List<string> lsterror = new List<string>();

            if (true)
            {
                var PreviousDatas = Context.CompanyMasters.Where(x => x.IsActive == 1).ToList();
                PreviousDatas.ForEach(x => { x.IsActive = 0; });
                Context.SaveChanges();

                CompanyMaster company = new CompanyMaster
                {
                    Name = entity.Name,
                    Website = entity.Website,
                    Phone = entity.Phone,
                    Fax = entity.Fax,
                    Email = entity.Email,
                    VatNumber = entity.VatNumber,
                    IsDefault = 1,
                    IsActive = 1
                };

                Context.CompanyMasters.Add(company);
                Context.SaveChanges();


                CreateCompanyBank(entity.CompanyMasterBankDetails,company.Id);


                transaction.Commit();
                Rslt.Id = company.Id;
                Rslt.ErrorCodes = lsterror;
            }



            return Rslt;
        }
        public void CreateCompanyBank(IEnumerable<CompanyMasterBankDetailEntity> BankDetails,int cmpnyId) {
        
            foreach (var bank in BankDetails) {
                CompanyMasterBankDetail bankDetail = new CompanyMasterBankDetail { 
                    CompanyMasterId = cmpnyId,
                    BankName = bank.BankName,
                    BankAccountName = bank.BankAccountName,
                    BankAccountNumber  = bank.BankAccountNumber,
                    Ibannumber = bank.Ibannumber,
                    BankIfsc = bank.BankIfsc,
                    IsActive=bank.IsActive,
                };
                Context.CompanyMasterBankDetails.Add(bankDetail);
                Context.SaveChanges();
            }
        }

        public IEnumerable<CompanyMasterBankDetailEntity> GetCompanyMasterBankDetails(int companyId)
        {
            var bankDetails = (from bank in Context.CompanyMasterBankDetails
                               where bank.IsActive == 1
                               && bank.CompanyMasterId == companyId
                               select new CompanyMasterBankDetailEntity
                               {
                                   Id   = bank.Id,
                                   CompanyMasterId  =  bank.CompanyMasterId,
                                   BankName= bank.BankName,
                                   BankAccountName  =   bank.BankAccountName,
                                   BankAccountNumber =bank.BankAccountNumber,
                                   BankIfsc = bank.BankIfsc,
                                   Ibannumber = bank.Ibannumber,
                                   IsActive=bank.IsActive,

                               }).ToList();
            return bankDetails;
        }
        public CompanyMasterEntity GetDefaultCompany()
        {
            var obj = (
                from cmpny in Context.CompanyMasters

                where cmpny.IsDefault == 1 &&
                    cmpny.IsActive == 1
                select new CompanyMasterEntity
                {
                    Id = cmpny.Id,
                    Name =cmpny.Name,
                    Website = cmpny.Website,
                    Phone = cmpny.Phone,
                    Fax = cmpny.Fax,
                    Email = cmpny.Email,
                    VatNumber = cmpny.VatNumber
                }).FirstOrDefault();

            obj.CompanyMasterBankDetails = GetCompanyMasterBankDetails(obj.Id);


            return obj;
        }

    }
}
