using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting Cli App...");
IUserRepository userRepo = new UserFileRepository();
IPostRepository postRepo = new PostFileRepository();
ICommentAsync commentRepo = new CommentFileRepository();

CliApp cliApp = new CliApp(userRepo, commentRepo, postRepo);
await cliApp.StartAsync();  