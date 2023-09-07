using AutoMapper;
using TestPlatform.Domain.Entities.Users;
using TestPlatform.Services.DTOs.UserDTOs;

namespace TestPlatform.Services.Mappers;

public class MapperConfigure : Profile
{
    public MapperConfigure()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto ,User>();
        CreateMap<UserForCreationDto, User>();
    }
}