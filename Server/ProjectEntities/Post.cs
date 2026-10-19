namespace ProjectEntities;

public class Post
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    // public DateTime CreatedAt { get; set; }


    public User Author { get; set; } = null!;
    public int LikesTotal { get; set; }
    public ICollection<Like> Likes { get; set; } = new List<Like>();

    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
}