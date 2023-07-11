using AutoMapper;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;

namespace ProjectOne.Api.Mapper.TaxGroup
{
    public class TaxGroupProfile : Profile
    {
        public TaxGroupProfile()
        {
            CreateMap<TaxGroupModel, TaxGroupEntity>();
            CreateMap<TaxGroupEntity, TaxGroupModel>();
            CreateMap<TaxGroupChildsModel, TaxGroupChildsEntity>();
            CreateMap<TaxGroupChildsEntity, TaxGroupChildsModel>();
        }

    }
}
