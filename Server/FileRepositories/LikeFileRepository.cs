using System.Text.Json;
using ProjectEntities;
using RepositoryContracts;

namespace FileRepositories;

public class LikeFileRepository : ILikeAsync
{
    private readonly string filePath = "likes.json";

    public LikeFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    private async Task<List<Like>> ReadLikesAsync()
    {
        string likesAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Like>>(likesAsJson) ?? new List<Like>();
    }

    private async Task SaveLikesAsync(List<Like> likes)
    {
        string updatedLikesAsJson = JsonSerializer.Serialize(likes);
        await File.WriteAllTextAsync(filePath, updatedLikesAsJson);
    }

    public async Task<Like> AddAsync(Like like)
    {
        List<Like> likes = await ReadLikesAsync();
        like.Id = likes.Count > 0 ? likes.Max(l => l.Id) + 1 : 1;
        likes.Add(like);
        await SaveLikesAsync(likes);
        return like;
    }

    public async Task UpdateAsync(Like like)
    {
        List<Like> likes = await ReadLikesAsync();
        Like? existingLike = likes.SingleOrDefault(l => l.Id == like.Id);
        if (existingLike is null)
        {
            throw new InvalidOperationException($"Like with ID '{like.Id}' not found");
        }

        int index = likes.IndexOf(existingLike);
        likes[index] = like;
        await SaveLikesAsync(likes);
    }

    public async Task DeleteAsync(int id)
    {
        List<Like> likes = await ReadLikesAsync();
        Like? likeToDelete = likes.SingleOrDefault(l => l.Id == id);
        if (likeToDelete is null)
        {
            throw new InvalidOperationException($"Like with ID '{id}' not found");
        }

        likes.Remove(likeToDelete);
        await SaveLikesAsync(likes);
    }

    public async Task<Like> GetSingleAsync(int id)
    {
        List<Like> likes = await ReadLikesAsync();
        Like? like = likes.SingleOrDefault(l => l.Id == id);
        if (like is null)
        {
            throw new InvalidOperationException($"Like with ID '{id}' not found");
        }

        return like;
    }

    public IQueryable<Like> GetMany()
    {
        string likesAsJson = File.ReadAllText(filePath);
        List<Like> likes = JsonSerializer.Deserialize<List<Like>>(likesAsJson) ?? new List<Like>();
        return likes.AsQueryable();
    }
}
