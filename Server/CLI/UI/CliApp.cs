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
            Console.WriteLine();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ManageUsersMenuAsync();
                    break;
                case "2":
                    await ManagePostsMenuAsync();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
    
    // =====================Menus===================
// Users
    private async Task ManageUsersMenuAsync()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Users ===");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. See all users");
            Console.WriteLine("3. List specific user");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                
                case "1":
                    await new CreateUserView(userRepo).CreateUserAsync();
                    break;
                case "2":
                    await new ListUsersView(userRepo).ListUsersAsync();
                    break;
                case "3":
                    await new SingleUserView(userRepo).SingleUserAsync();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
    //Posts
    private async Task ManagePostsMenuAsync()
    {
        bool back = false;
        while (!back) 
        {
            Console.WriteLine();
            Console.WriteLine("=== Manage Posts ===");
            Console.WriteLine("1. Create new post");
            Console.WriteLine("2. See all posts");
            Console.WriteLine("3. Edit post");
            Console.WriteLine("4. Delete post");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreatePostView(postRepo, userRepo).CreatePostAsync();
                    break;
                case "2":
                    await new ListPostsView(postRepo, userRepo).ListPostsAsync();
                    break;
                case "3":
                    Console.WriteLine("select Id of post to edit");
                    int editId = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter new title:");
                    string newTitle = Console.ReadLine()!;
                    Console.WriteLine("Enter new body:");
                    string newBody = Console.ReadLine()!;
                    await new ManagePostsView(postRepo, userRepo).EditPostAsync(editId, newTitle, newBody);
                    break;
                case "4":
                    Console.WriteLine("select Id of post to delete");
                    int deleteId = int.Parse(Console.ReadLine()!);
                    await new ManagePostsView(postRepo, userRepo).DeletePostAsync(deleteId);
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
   
}