namespace ProjectEntities;

public class User(int id, string username, string password)
{
    public User() : this(0, "", "")
    {
        throw new NotImplementedException();
    }

    public int Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;

    public override string ToString()
    {
        return $"ID : {Id}, Username : {Username}";
    }
}