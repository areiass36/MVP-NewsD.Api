using System.Security.Policy;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsD.DataAccess;
using NewsD.DataContracts;
using NewsD.Model;
using static BCrypt.Net.BCrypt;

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
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] DataContracts.UserRequest userRequest)
    {
        var user = await _context.Users!.SingleOrDefaultAsync(u => u.Email == userRequest.Email);

        if (user is not null && Verify(userRequest.Password, user.Password))
            return Ok(_mapper.Map<UserResponse>(user));

        return BadRequest("Email ou senha errados");
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] DataContracts.UserRequest userContract)
    {
        var user = _mapper.Map<Model.User>(userContract);

        var exists = await _context.Users!.CountAsync(u => u.Email == user.Email) >= 1;

        if (exists) return BadRequest("Já existe um usuário cadastrado com esse email");

        await _context.Users!.AddAsync(user);
        await _context.SaveChangesAsync();

        var returnedUser = _mapper.Map<DataContracts.UserResponse>(user);

        return Ok(returnedUser);
    }
}