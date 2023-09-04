using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.Models;

namespace TestPlatform.Services.UserServices;

public interface IUserService
{
    ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto);
    IQueryable<UserDto> RetrieveUsers(QueryParameter queryParameter);
    ValueTask<UserDto> RetrieveUserByIdAsync(Guid userId);
    ValueTask<UserDto> RemoveUserAsync(Guid userId);
}