using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentAsync
{
    public List<Comment> Comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = Comments.Any() ? Comments.Max(c => c.Id) + 1 : 1;
        Comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existing = Comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existing is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }

        Comments.Remove(existing);
        Comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? comment = Comments.SingleOrDefault(c => c.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        Comments.Remove(comment);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = Comments.SingleOrDefault(c => c.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany()
    {
        return Comments.AsQueryable();
    }
}