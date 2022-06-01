namespace NewsD.DataContracts;

public class NewsResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public UserResponse? Creator { get; set; }
}