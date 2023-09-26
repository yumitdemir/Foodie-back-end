using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> Create(UserCreateDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            UserName = request.UserName
        };
        user = await _userRepository.Create(user);
        return Ok(user);
    }
}