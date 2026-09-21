using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    public List<Post> Posts = new List<Post>();

    public PostInMemoryRepository()
    {
        Posts.AddRange(new[]
        {
            new Post { Id = 1, AuthorId = 1, Title = "Welcome post", Body = "This is the first sample post.", Author = new User { Id = 1, Username = "alice", Password = "password1" } },
            new Post { Id = 2, AuthorId = 2, Title = "Second post", Body = "This is a second sample post.", Author = new User { Id = 2, Username = "bob", Password = "password2" } },
            new Post { Id = 3, AuthorId = 3, Title = "Third post", Body = "This is the third sample post.", Author = new User { Id = 3, Username = "charlie", Password = "password3" } }
        });
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = Posts.Any()
            ? Posts.Max(p => p.Id) + 1
            : 1;
        Posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = Posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{post.Id}' not found");
        }

        Posts.Remove(existingPost);
        Posts.Add(post);

        return Task.CompletedTask;        
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = Posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        Posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        // Do implementation
        Post? postToGet = Posts.SingleOrDefault(p => p.Id == id);
        if (postToGet is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany()
    {
        return Posts.AsQueryable();
    }
}