using System.Text.Json;
using ProjectEntities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<User>> ReadUsersAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    }

    private async Task SaveUsersAsync(List<User> users)
    {
        string updatedUsersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, updatedUsersAsJson);
    }

    public async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadUsersAsync();
        user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        await SaveUsersAsync(users);
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users = await ReadUsersAsync();
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        int index = users.IndexOf(existingUser);
        users[index] = user;
        await SaveUsersAsync(users);
    }

    public async Task DeleteAsync(int id)
    {
        List<User> users = await ReadUsersAsync();
        User? userToDelete = users.SingleOrDefault(u => u.Id == id);
        if (userToDelete is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(userToDelete);
        await SaveUsersAsync(users);
    }

    public async Task<User> GetSingleAsync(string username)
    {
        List<User> users = await ReadUsersAsync();
        User? user = users.SingleOrDefault(u => u.Username == username);
        if (user is null)
        {
            throw new InvalidOperationException($"User with UserName '{username}' not found");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        string usersAsJson = File.ReadAllText(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
        return users.AsQueryable();
    }

    public async Task<User> GetByIdAsync(int authorId)
    {
        List<User> users = await ReadUsersAsync();
        User? user = users.SingleOrDefault(u => u.Id == authorId);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{authorId}' not found");
        }

        return user;
    }
}
