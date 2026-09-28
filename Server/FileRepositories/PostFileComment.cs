using System.Text.Json;
using ProjectEntities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Post>> ReadPostsAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
    }

    private async Task SavePostsAsync(List<Post> posts)
    {
        string updatedPostsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, updatedPostsAsJson);
    }

    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadPostsAsync();
        post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        await SavePostsAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadPostsAsync();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }

        int index = posts.IndexOf(existingPost);
        posts[index] = post;
        await SavePostsAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadPostsAsync();
        Post? postToDelete = posts.SingleOrDefault(p => p.Id == id);
        if (postToDelete is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        posts.Remove(postToDelete);
        await SavePostsAsync(posts);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await ReadPostsAsync();
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        string postsAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
        return posts.AsQueryable();
    }
}