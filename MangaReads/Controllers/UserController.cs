using MangaReads.Classes;
using MangaReads.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public ActionResult<User> GetUser(string userName)
    {
        var user = _userService.GetUser(userName);

        return user == null ?  new NotFoundResult() : user;
    }
    
    [HttpPost("CreateUser")]
    public void CreateUser(string userName)
    {
        //TODO might want to return something to let the user know it worked
        _userService.CreateUser(userName);
    }
    
    [HttpPost("UpdateUserMangaReadingStatus")]
    public void UpdateUserMangaReadingStatus(string userName, string mangaName, string status)
    {
        _userService.UpdateUserMangaReadingStatus(userName, mangaName, status);
    }
    
    [HttpPost("AddUserManga")]
    public void AddUserManga(string userName, string mangaName)
    {
        _userService.AddUserManga(userName, mangaName);
    }
    
    [HttpPost("UpdateUserMangaVolumeNumber")]
    public void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber)
    {
        _userService.UpdateUserMangaVolumeNumber(userName, mangaName, newVolumeNumber);
    }
    
    [HttpDelete("DeleteUserManga")]
    public void DeleteUserManga(string userName, string mangaName)
    {
        _userService.DeleteUserManga(userName, mangaName);
    }
    
    [HttpDelete("DeleteUser")]
    public void DeleteUser(string userName)
    {
        _userService.DeleteUser(userName);
    }
    
}