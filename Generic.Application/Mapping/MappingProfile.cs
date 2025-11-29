using AutoMapper;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.DTO_s.Response.User;
using Generic.Domain.Entities;

namespace Generic.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            RequestToDomainMappings();
            DomainToResponseMappings();
        }

        private void RequestToDomainMappings()
        {
            //User
            CreateMap<RequestRegisterUserJson, UserEntity>();
        }

        private void DomainToResponseMappings()
        {
            //User
            CreateMap<UserEntity, ResponseLoggedUserJson>();
        }
    }
}
