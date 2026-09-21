using CLI.UI.ManagePost;
using CLI.UI.ManageUser;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp(IUserRepository userRepo, ICommentAsync commentRepo, IPostRepository postRepo)
{
    public async Task StartAsync()
    {
        _ = commentRepo;

        bool running = true;
        while (running)
        {
            Console.WriteLine("Welcome to the CLI App!");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. Create User");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var createPostView = new CreatePostView(postRepo);
                    await createPostView.CreatePostAsync();
                    break;
                case "2":
                    var createUser = new CreateUserView(userRepo);
                    await createUser.CreateUserAsync();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}