namespace NewsD.Model;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public UserRole Role { get; set; }
    public IEnumerable<News>? News { get; set; }
}

public enum UserRole
{
    Normal = 1,
    Analyzer = 2
}