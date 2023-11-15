using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace MangaReads.Controllers;

[ApiController]
[Route("[controller]")]
public class MangaController : ControllerBase
{
    private readonly ILogger<MangaController> _logger;

    public MangaController(ILogger<MangaController> logger)
    {
        _logger = logger;
    }

    private string _mangaDexUrl = "https://api.consumet.org/manga/mangadex/";
    private string _mangaDexInfoUrl = "https://api.consumet.org/manga/mangadex/info/";

    HttpClient client = new();
    
    
    [HttpGet(Name = "GetMangaSearch")]
    public async Task<string> Get(string mangaName)
    {
        mangaName = mangaName.ToLower();
        
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        return await client.GetStringAsync(_mangaDexUrl + mangaName);
    }
}