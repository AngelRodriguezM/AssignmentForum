using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class LikeInMemoryRepository : ILikeAsync
{
    public List<Like> Likes = new List<Like>();

    public LikeInMemoryRepository()
    {
       
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