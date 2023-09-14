using System.Collections.Generic;
using TestPlatform.Domain.Entities.Users;
using TestPlatform.Domain.Exceptions;
using TestPlatform.Infrastructure.Authentication;
using TestPlatform.Infrastructure.Repositories.Users;
using TestPlatform.Services.DTOs.UserDTOs;
using TestPlatform.Services.UserServices;
using TestPlatform.Testing.MockRepository;

namespace TestPlatform.Testing.TestUserServices;

[TestClass]
public class UnitTestCreateUser
{
    private IUserService _userService;

    [TestInitialize]
    public void Initialize()
    {
        IUserRepository _repository = new UserRepo(new List<User>());
        IPasswordHasher _passwordHasher = new PasswordHasher();
        _userService = new UserService(_repository,_passwordHasher);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _userService = null;
    }

    [TestMethod]
    public void PositiveTest()
    {
        var user = new UserForCreationDto("hello","hello","12345678","hello@as.as");
        var user2 = _userService.CreateUserAsync(user).Result;
        Assert.IsNotNull(user2);
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidModelException))]
    public void InvalidModelAllTest()
    {
        var user = new UserForCreationDto(null, null, null, null);
        var user2 = _userService.CreateUserAsync(user).Result;
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidModelException))]
    public void InvalidModelEmailTest()
    {
        var user = new UserForCreationDto("hello", "hello", "12345678", null);
        var user2 = _userService.CreateUserAsync(user).Result;
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidModelException))]
    public void InvalidModelPasswordTest()
    {
        var user = new UserForCreationDto("hello", "hello", null, "hello@as.as");
        var user2 = _userService.CreateUserAsync(user).Result;
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidModelException))]
    public void InvalidModelUsernameTest()
    {
        var user = new UserForCreationDto(null, "hello", "12345678", "hello@as.as");
        var user2 = _userService.CreateUserAsync(user).Result;
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidEmailException))]
    public void InvalidEmailTest()
    {
        var user = new UserForCreationDto("hello", "hello", "12345678", "hello");
        var user2 = _userService.CreateUserAsync(user).Result;
    }
}
