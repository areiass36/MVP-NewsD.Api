using System.ComponentModel.DataAnnotations.Schema;

namespace NewsD.Model;

public class NewsStatus
{
    public Guid Id { get; set; }
    public Guid UpdatedById { get; set; }
    public Guid NewsId { get; set; }
    public NewsStatusType Status { get; set; }
    public User? UpdatedBy { get; set; }
    public DateTime When { get; set; }
    public string? Reason { get; set; }
}

public enum NewsStatusType
{
    Created = 1,
    Analyzing = 2,
    Ready = 3,
    NeedVerification = 4,
    Deleted = 5,
    Updated = 6
}