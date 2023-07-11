using AutoMapper;
using ProjectOne.Data.Entitiy;
using ProjectOne.Models;

namespace ProjectOne.Api.Mapper.Login
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile() {
            CreateMap<UserLoginEntity, UserLoginModel>();
            CreateMap<UserLoginModel, UserLoginEntity>();
        }
    }
}
