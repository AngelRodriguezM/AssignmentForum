using ProjectEntities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentAsync
{
    public List<Comment> Comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        Comments.AddRange(new[]
        {
            new Comment { Id = 1, UserId = 2, PostId = 1, Body = "Nice post!", User = new User { Id = 2, Username = "bob", Password = "password2" }, Post = new Post { Id = 1, AuthorId = 1, Title = "Welcome post", Body = "This is the first sample post.", Author = new User { Id = 1, Username = "alice", Password = "password1" } } },
            new Comment { Id = 2, UserId = 3, PostId = 1, Body = "I agree.", User = new User { Id = 3, Username = "charlie", Password = "password3" }, Post = new Post { Id = 1, AuthorId = 1, Title = "Welcome post", Body = "This is the first sample post.", Author = new User { Id = 1, Username = "alice", Password = "password1" } } },
            new Comment { Id = 3, UserId = 1, PostId = 2, Body = "Thanks for sharing.", User = new User { Id = 1, Username = "alice", Password = "password1" }, Post = new Post { Id = 2, AuthorId = 2, Title = "Second post", Body = "This is a second sample post.", Author = new User { Id = 2, Username = "bob", Password = "password2" } } }
        });
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