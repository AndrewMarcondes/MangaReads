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

    private const string MangaDexUrl = "https://api.consumet.org/manga/mangadex/";
    private const string MangaDexInfoUrl = "https://api.consumet.org/manga/mangadex/info/";

    HttpClient _client = new();

    public class MangaVolume
    {
        private int id;
        private int volumeId;
        private string fileName;
    }
    
    
    [HttpGet(Name = "GetMangaSearch")]
    public async Task<string> Get(string mangaName)
    {
        mangaName = mangaName.ToLower();
        
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        return await _client.GetStringAsync(MangaDexUrl + mangaName);
    }

    public async Task<MangaVolume[]> GetMangaVolumeInfo(int mangaId)
    {
        try
        {
            const string url1 = "https://api.mangadex.org/cover?manga%5B%5D=";
            const string url2 = "&order%5BcreatedAt%5D=asc&order%5BupdatedAt%5D=asc&order%5Bvolume%5D=asc&limit=100";

            var getVolumesInfo = await _client.GetStringAsync(url1 + mangaId + url2);

            var volumeData = new MangaVolume[getVolumesInfo.Length];
            
            // getVolumesInfo.data.forEach(volume =>
            // {
            //     var volumeConverted =
            //     {
            //         id: volume.id,
            //         volumeNumber: volume.attributes.volume,
            //         fileName: volume.attributes.fileName,
            //     }
            //     volumeData.push(volumeConverted)
            // });

            return volumeData;
        }
        catch (Exception error)
        {
            // console.log("error")
            // console.log(error)
            // res.json({error, message: `Unable to fetch data on mangaVolumeInfo`})
            //
            return Array.Empty<MangaVolume>();
        }
    }
}