using AutoMapper;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;

namespace ProjectOne.Api.Mapper.Login
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile() {
            CreateMap<InvoiceHdrEntity, InvoiceHdrModel>();
            CreateMap<InvoiceHdrModel, InvoiceHdrEntity>();

            CreateMap<InvoiceContentEntity, InvoiceContentModel>();
            CreateMap<InvoiceContentModel, InvoiceContentEntity>();

            CreateMap<InvoiceCustomerDetailModel, InvoiceCustomerDetailEntity>();
            CreateMap<InvoiceCustomerDetailEntity, InvoiceCustomerDetailModel>();

            CreateMap<InvoiceAmountModel, InvoiceAmountEntity>();
        }
    }
}
