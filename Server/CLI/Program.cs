using CLI.UI;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting Cli App...");
IUserRepository userRepo = new UserInMemoryRepository();
IPostRepository postRepo = new PostInMemoryRepository();
ICommentAsync commentRepo = new CommentInMemoryRepository();

CliApp cliApp = new CliApp(userRepo, commentRepo, postRepo);
await cliApp.StartAsync();  