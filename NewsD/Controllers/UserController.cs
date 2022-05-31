using Microsoft.AspNetCore.Mvc;
using NewsD.DataAccess;

namespace NewsD.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly NewsDDataContext _context;

    public UserController(NewsDDataContext context) => this._context = context!;


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _context.Users!.SingleOrDefault(u => u.Id == id);
        return Ok(user);
    }
}