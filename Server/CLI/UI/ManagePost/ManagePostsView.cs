using ProjectEntities;
using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class ManagePostsView(IPostRepository postRepo, IUserRepository userRepo)
{
    public async Task EditPostAsync(int posId, string title, string body)
    {
        Post post = await postRepo.GetSingleAsync(posId);
        post.Title = title;
        post.Body = body;
        await postRepo.UpdateAsync(post);
    }

    public async Task DeletePostAsync(int postId)
    {
        await postRepo.DeleteAsync(postId);
        Console.WriteLine("Post deleted successfully.");
    }
}