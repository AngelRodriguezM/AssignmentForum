using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class ListUsersView(IUserRepository userRepo)
{
    public async Task ListUsersAsync()
    {
        Console.WriteLine("=====List Users========");

        var users = userRepo.GetMany().ToList();
        
        //print

        foreach (var user in users)
        {
            int count = 1;
            Console.WriteLine( $"{count}."+ $"User: {user.Username} Id: {user.Id}");
            count++;
        }
        
        
    }
}