using ProjectEntities;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class CreateUserView(IUserRepository userRepo)
{
    public async Task CreateUserAsync()
    {
        Console.WriteLine("======Create New User=======");

        Console.WriteLine("Username : ");
        string? username = Console.ReadLine();

        Console.WriteLine("Password :");
        string? password = Console.ReadLine();

        int id = Random.Shared.Next();
        
        // Validationss

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Invalid input. Please enter both username and password.");
            return;
        }

        bool usernameExists =
            userRepo.GetMany().Any(u => u.Username == username);
        if (usernameExists)
        {
            Console.WriteLine("Username Already Exists!");
            return;
        }

        bool idExists = userRepo.GetMany().Any(u => u.Password == password);
        if (idExists)
        {
            Console.WriteLine("Password Already Exists!");
            return;
        }
        //create user

        User user = new User(id, username, password)
        {
            Username = username,
            Password = password
        };

        await userRepo.AddAsync(user);

        Console.WriteLine("User created: " + user.Username);

    }
}