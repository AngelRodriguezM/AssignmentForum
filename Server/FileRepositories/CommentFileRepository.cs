using System.Text.Json;
using ProjectEntities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository  : ICommentAsync
{
    private readonly string filePath = "comments.json";

    private async Task<string> getCommentsAsJson()
    {
        return await File.ReadAllTextAsync(filePath);
    }
    
    private async Task<List<Comment>> deserializeJSON(string commentsAsJSON)
    {
        List<Comment> comments = new List<Comment>();
     return   comments =
         JsonSerializer.Deserialize<List<Comment>>(commentsAsJSON)!;
    }

    public CommentFileRepository() 
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        // Read all text from comments.json asynchronously.
        // At this point, the content is JSON, for example:
        // [{"Id":1,"Text":"Hello"}]
        string commentsAsJSON = await getCommentsAsJson();
        // Convert/deserialise the JSON text into a C# List<Comment>.
        // The ! tells C# that we expect the result not to be null.
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJSON)!;
        // Find the largest existing comment ID.
        // If there are no comments yet, use 1 as the starting value.
        int maxID = comments.Count > 0
            ? comments.Max(c => c.Id)
            : 1;
        comment.Id = maxID + 1;
        comments.Add(comment);

        string updatedCommentsAsJSON = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, updatedCommentsAsJSON);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJSON = await getCommentsAsJson();
        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJSON)!;
        Comment? commentToEdit =
            comments.FirstOrDefault(c => c.Id == comment.Id);
        if (commentToEdit == null)
        {
            throw new Exception(
                $"Comment with ID Not found");
        }

        int index = comments.IndexOf(commentToEdit);
        comments[index] = comment;

        string updatedCommentsAsJson =
            JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, updatedCommentsAsJson);
        



    }

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await getCommentsAsJson();
        List<Comment> comments = await deserializeJSON(commentsAsJson);
        
        Comment? commentToDelete = comments.FirstOrDefault(c => c.Id == id);
        if (commentToDelete != null)
        {
            throw new Exception($"Comment with ID {id} not found");
            
        }

        comments.Remove(commentToDelete);

        string updatedCommentsAsJSON =
            JsonSerializer.Serialize(comments);


        await File.WriteAllTextAsync(
            filePath,
            updatedCommentsAsJSON
        );
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await getCommentsAsJson();
        List<Comment> comments = await deserializeJSON(commentsAsJson);
        
        Comment commentToGet = comments.FirstOrDefault(c => c.Id == id);

        return (commentToGet);

    }

    public IQueryable<Comment> GetMany()
    {
        // GetMany is synchronous because your interface
        // returns IQueryable<Comment>, not Task<IQueryable<Comment>>

        string commentsAsJSON =
            File.ReadAllText(filePath);

        List<Comment> comments =
            JsonSerializer.Deserialize<List<Comment>>(commentsAsJSON)!;

        // Convert the List<Comment> into IQueryable<Comment>
        return comments.AsQueryable();
    }
}