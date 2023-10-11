using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TestPlatform.Domain.Entities.Users;
using TestPlatform.Domain.Exceptions;
using TestPlatform.Infrastructure.Authentication;
using TestPlatform.Infrastructure.Repositories.Users;
using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.Models;

namespace TestPlatform.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;

        _mapper = Mappers.AutoMapper.Mapper;
    }

    public async ValueTask<UserDto> CreateUserAsync(UserForCreationDto userForCreationDto)
    {
        var user = _mapper.Map<UserForCreationDto,User>(userForCreationDto);
        var userBase = await _userRepository.SelectByEmailAsync(userForCreationDto.email);
        if (userBase is not null)
            throw new RegisteredException($"Registered Email: {userForCreationDto.email}");
        user.PasswordHash = _passwordHasher.Encrypt(userForCreationDto.password, user.Salt);
        user = await _userRepository.InsertAsync(user);
        return _mapper.Map<User,UserDto>(user);
    }

    public IQueryable<UserDto> RetrieveUsers(QueryParameter queryParameter)
    {
        return _userRepository.SelectAll()
            .Skip(queryParameter.Page.Index < 1 ? 0 : (queryParameter.Page.Index - 1) * queryParameter.Page.Size)
            .Take(queryParameter.Page.Size).Select(u => _mapper.Map<User , UserDto>(u));
    }

    public async ValueTask<UserDto> RetrieveUserByIdAsync(long userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId);

        return _mapper.Map<User ,UserDto>(user);
    }

    public async ValueTask<UserDto> RemoveUserAsync(long userId)
    {
        var user = await _userRepository.SelectByIdAsync(userId)
                   ?? throw new NotFoundException($"Not Found by {userId} in user");
      
        user = await _userRepository.DeleteAsync(user);

        return _mapper.Map<User, UserDto>(user);
    }
}