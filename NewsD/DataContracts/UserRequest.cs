using NewsD.Model;

namespace NewsD.DataContracts;
public class UserRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole Role { get; set; }

}