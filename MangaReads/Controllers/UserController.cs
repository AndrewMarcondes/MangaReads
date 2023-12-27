using MangaReads.Classes;
using MangaReads.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Controllers;


[ApiController]
[Route("user/")]
public class UserController
{
    private readonly ILogger<UserController> _logger;

    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet("GetUser")]
    public Task<User> GetUser(string userName)
    {
        return _mangaService.MangaSearch(mangaName);
    }
}