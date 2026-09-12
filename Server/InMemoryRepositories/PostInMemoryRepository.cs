using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    public List<Post> Posts = new List<Post>();
    
     
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = Posts.Any() //first we set the id
            ? Posts.Max(p => p.Id) + 1 // by finding the current max Id and adding one
            : 1;// else, just use id =  1 //weird looking if else i dont fully understand yet
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