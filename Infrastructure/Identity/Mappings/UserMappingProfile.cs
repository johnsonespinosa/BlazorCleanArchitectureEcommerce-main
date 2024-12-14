using Application.Features.Users.Commands.CreateUser;
using AutoMapper;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, User>();
        }
    }
}
