using TestPlatform.Domain.Entities.Users;
using TestPlatform.Infrastructure.Repositories.Users;

namespace TestPlatform.Testing.MockRepository;

class UserRepo : IUserRepository
{
    private List<User> _users;
    private long _id;

    public UserRepo(List<User> users)
    {
        _users = users;
        if (users.Count > 0)
            _id = _users.Max(u => u.Id) + 1;
        else
            _id = 1;
    }

    public async ValueTask<User> InsertAsync(User entity)
    {
        entity.Id = _id++;
        _users.Add(entity);
        return await Task.FromResult(entity);
    }

    public IQueryable<User> SelectAll()
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> SelectByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public ValueTask<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<User?> SelectByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}