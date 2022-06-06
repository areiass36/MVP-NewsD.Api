using NewsD.Model;

namespace NewsD.DataContracts;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public UserRole Role { get; set; }
}