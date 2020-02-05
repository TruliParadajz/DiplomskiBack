using BackendApi.Entities;
using BackendApi.Models;
using AutoMapper;

namespace BackendApi.Helpers_and_Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
