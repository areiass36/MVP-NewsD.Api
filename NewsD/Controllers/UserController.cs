using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsD.DataAccess;
using NewsD.DataContracts;

namespace NewsD.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly NewsDDataContext _context;
    private readonly IMapper _mapper;

    public UserController(NewsDDataContext context, IMapper mapper)
    {
        _context = context!;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUser(Guid id)
    {
        var user = _context.Users!.SingleOrDefault(u => u.Id == id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] DataContracts.UserRequest userContract)
    {
        var user = _mapper.Map<Model.User>(userContract);

        var exists = await _context.Users!.CountAsync(u => u.Email == user.Email) >= 1;

        if (exists) return BadRequest("User with given email already exists");

        await _context.Users!.AddAsync(user);
        await _context.SaveChangesAsync();

        var returnedUser = _mapper.Map<DataContracts.UserResponse>(user);

        return Ok(returnedUser);
    }
}