using System.ComponentModel.DataAnnotations.Schema;

namespace NewsD.Model;

public class News
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public int Likes { get; set; }
    public string[]? Source { get; set; }
    public string[]? Topics { get; set; }
    public User? Creator { get; set; }
}