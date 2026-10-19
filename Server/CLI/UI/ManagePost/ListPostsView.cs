using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class ListPostsView(IPostRepository postRepo, IUserRepository userRepo )
{
      public async Task ListPostsAsync()
      {
            Console.WriteLine("=========List Posts=========\n");
            var posts = postRepo.GetMany().ToList();
            foreach (var post in posts)
            {
                  int count = 1;
                  Console.WriteLine($"{count}. {post.Title}");
                  count++;
            }
      }
}