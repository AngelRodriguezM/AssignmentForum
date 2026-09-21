using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    public List<User> Users = new List<User>();

    public UserInMemoryRepository()
    {
        Users.AddRange(new[]
        {
            new User { Id = 1, Username = "alice", Password = "password1" },
            new User { Id = 2, Username = "bob", Password = "password2" },
            new User { Id = 3, Username = "charlie", Password = "password3" }
        });
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
        Users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existing = Users.SingleOrDefault(u => u.Id == user.Id);
        if (existing is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        Users.Remove(existing);
        Users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? user = Users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        Users.Remove(user);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = Users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return Users.AsQueryable();
    }
}