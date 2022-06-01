namespace NewsD.DataContracts;

public class NewsRequest
{
    public Guid CreatorId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public IFormFile? Image { get; set; }

}