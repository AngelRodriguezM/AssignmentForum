using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class LikeInMemoryRepository : ILikeAsync
{
    public List<Like> Likes = new List<Like>();

    public LikeInMemoryRepository()
    {
        Likes.AddRange(new[]
        {
            new Like { Id = 1, UserId = 1, PostId = 1, User = new User { Id = 1, Username = "alice", Password = "password1" }, Post = new Post { Id = 1, AuthorId = 1, Title = "Welcome post", Body = "This is the first sample post.", Author = new User { Id = 1, Username = "alice", Password = "password1" } } },
            new Like { Id = 2, UserId = 2, PostId = 1, User = new User { Id = 2, Username = "bob", Password = "password2" }, Post = new Post { Id = 1, AuthorId = 1, Title = "Welcome post", Body = "This is the first sample post.", Author = new User { Id = 1, Username = "alice", Password = "password1" } } },
            new Like { Id = 3, UserId = 3, PostId = 2, User = new User { Id = 3, Username = "charlie", Password = "password3" }, Post = new Post { Id = 2, AuthorId = 2, Title = "Second post", Body = "This is a second sample post.", Author = new User { Id = 2, Username = "bob", Password = "password2" } } }
        });
    }

    public Task<Like> AddAsync(Like like)
    {
        like.Id = Likes.Any() ? Likes.Max(l => l.Id) + 1 : 1;
        Likes.Add(like);
        return Task.FromResult(like);
    }

    public Task UpdateAsync(Like like)
    {
        Like? existing = Likes.SingleOrDefault(l => l.Id == like.Id);
        if (existing is null)
        {
            throw new InvalidOperationException($"Like with ID '{like.Id}' not found");
        }

        Likes.Remove(existing);
        Likes.Add(like);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Like? like = Likes.SingleOrDefault(l => l.Id == id);
        if (like is null)
        {
            throw new InvalidOperationException($"Like with ID '{id}' not found");
        }

        Likes.Remove(like);
        return Task.CompletedTask;
    }

    public Task<Like> GetSingleAsync(int id)
    {
        Like? like = Likes.SingleOrDefault(l => l.Id == id);
        if (like is null)
        {
            throw new InvalidOperationException($"Like with ID '{id}' not found");
        }

        return Task.FromResult(like);
    }

    public IQueryable<Like> GetMany()
    {
        return Likes.AsQueryable();
    }
}