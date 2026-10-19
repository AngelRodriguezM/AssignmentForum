using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class SingleUserView(IUserRepository userRepo)
{
    public async Task SingleUserAsync()
    {
        Console.WriteLine("==========Get Single User=========");

        Console.WriteLine("input username to get: ");
        string userInput = Console.ReadLine().ToLower();
        
        // Validation

        bool userExists = userRepo.GetMany().Any(u => u.Username.ToLower() == userInput);
        if (!userExists)
        {
            Console.WriteLine("User Not Found!");
            return;
        }
        
        //get User
        
        var user = await userRepo.GetSingleAsync(userInput);
        Console.WriteLine($"User found: {user.Username}");

    }
}