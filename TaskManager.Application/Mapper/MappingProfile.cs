using AutoMapper;
using TaskManager.Application.Commands.User;
using TaskManager.Application.Response;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, CreateUserResponse>().ReverseMap();
        }
    }
}
