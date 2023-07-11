using AutoMapper;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;

namespace ProjectOne.Api.Mapper.CompanyMaster
{
    public class CompanyMasterProfile : Profile
    {
        public CompanyMasterProfile() { 
            CreateMap<CompanyMasterEntity,CompanyMasterModel>();
            CreateMap<CompanyMasterModel, CompanyMasterEntity>();
            CreateMap<CompanyMasterBankDetailModel, CompanyMasterBankDetailEntity>();
            CreateMap<CompanyMasterBankDetailEntity, CompanyMasterBankDetailModel>();
        }
    }
}
