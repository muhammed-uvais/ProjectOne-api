using AutoMapper;
using ProjectOne.Models;
using ProjectOne.Data.DbEntities;

namespace ProjectOne.Api.Mapper.TaxType
{
    public class TaxTypeProfile : Profile
    {
        public TaxTypeProfile() {
            CreateMap<TaxTypeModel, ProjectOne.Data.DbEntities.TaxType>();
            CreateMap<ProjectOne.Data.DbEntities.TaxType, TaxTypeModel>();
        } 
    }
}
