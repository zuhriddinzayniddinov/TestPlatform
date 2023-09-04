using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.Models;

namespace TestPlatform.Services.UserServices;

public class UserService : IUserService
{
    public ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        throw new NotImplementedException();
    }

    public IQueryable<UserDto> RetrieveUsers(QueryParameter queryParameter)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserDto> RetrieveUserByIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserDto> RemoveUserAsync(long userId)
    {
        throw new NotImplementedException();
    }
}