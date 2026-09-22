using RepositoryContracts;

namespace CLI.UI.ManagePost;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task CreatePostAsync()
    {
        Console.Write("Enter author ID: ");
        string authorIdInput = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter title: ");
        string title = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter body: ");
        string body = Console.ReadLine() ?? string.Empty;

        if (!int.TryParse(authorIdInput, out int authorId) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
        {
            Console.WriteLine("Author ID, title and body are required.");
            return;
        }

        var post = new ProjectEntities.Post
        {
            AuthorId = authorId,
            Title = title,
            Body = body,
            Author = await userRepository.GetByIdAsync(authorId)
        };

        var createdPost = await postRepository.AddAsync(post);
        Console.WriteLine($"Post created successfully with ID {createdPost.Id}.");
    }
}