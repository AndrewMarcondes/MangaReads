using MangaReads.Classes;
using MangaReads.Controllers;
using MangaReads.Interfaces;

namespace MangaReads.Services;

public class MangaJsonService
{
    private readonly ILogger<MangaJsonService> _logger;

    private readonly IMangaService _mangaService;

    private readonly IUserService _userService;

    public MangaJsonService(ILogger<MangaJsonService> logger, IMangaService mangaService, IUserService userService)
    {
        _logger = logger;
        _mangaService = mangaService;
        _userService = userService;
    }
    
    // try to use the two jsons (user and manga) to get the manga information without doing a fresh search every time
    
    // Mangas need to have a thirdpartyId and a volumeCount 
    
    
    public async Task<List<Manga>> GetAllUserManga(string userName)
    {
        var user = _userService.GetUser(userName);

        foreach (var manga in user.mangas)
        {
            
        }
        
        // TODO 
        
        
        var heck = new List<Manga>();
        
        return heck;
    }
    
}