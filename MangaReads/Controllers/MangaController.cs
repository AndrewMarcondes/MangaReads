using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using MangaReads.Classes;
using MangaReads.DTOs;
using MangaReads.Interfaces;
using Volume = MangaReads.DTOs.Volume;

namespace MangaReads.Controllers;

[ApiController]
[Route("manga/")]
public class MangaController : ControllerBase
{
    private readonly ILogger<MangaController> _logger;

    private readonly IMangaService _mangaService;

    public MangaController(ILogger<MangaController> logger, IMangaService mangaService)
    {
        _logger = logger;
        _mangaService = mangaService;
    }

    private const string MangaDexUrl = "https://api.mangadex.org/";
    private const string MangaDexInfoUrl = "https://api.consumet.org/manga/mangadex/info/";

    HttpClient _client = new();
    
    public struct image
    {
        public string imageName { get; set; } 
        public string imageUrl { get; set; } 
    }
    
    
    [HttpGet("GetMangaSearch")]
    public Task<List<MangaSearch>> GetMangaSearch(string mangaName)
    {
        return _mangaService.MangaSearch(mangaName);
    }
    
    [HttpGet("GetMangaInformation")]
    public async Task<MangaSearch> GetMangaInformation(string mangaId)
    {
        return await _mangaService.GetMangaInformation(mangaId);
    }
    
    [HttpGet("GetMangaVolumeInformation")]
    public async Task<List<Volume>> GetMangaVolumeInformation(string mangaId)
    {
        return await _mangaService.GetMangaVolume(mangaId);
    }
    
    [HttpGet("GetMangaVolumeImage")]
    public JsonResult GetMangaVolumeImage(string mangaId, string coverId)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        var jsonResponse = new image()
        {
            imageName = "image",
            imageUrl = "https://uploads.mangadex.org/covers/" + mangaId + "/" + coverId
        };

        return new JsonResult(jsonResponse);
    }
    
    // public async Task<Volume[]> GetMangaVolumeInfo(int mangaId)
    // {
    //     try
    //     {
    //         const string url1 = "https://api.mangadex.org/cover?manga%5B%5D=";
    //         const string url2 = "&order%5BcreatedAt%5D=asc&order%5BupdatedAt%5D=asc&order%5Bvolume%5D=asc&limit=100";
    //
    //         var getVolumesInfo = await _client.GetStringAsync(url1 + mangaId + url2);
    //
    //         var volumeData = new Volume[getVolumesInfo.Length];
    //         
    //         // getVolumesInfo.data.forEach(volume =>
    //         // {
    //         //     var volumeConverted =
    //         //     {
    //         //         id: volume.id,
    //         //         volumeNumber: volume.attributes.volume,
    //         //         fileName: volume.attributes.fileName,
    //         //     }
    //         //     volumeData.push(volumeConverted)
    //         // });
    //
    //         return volumeData;
    //     }
    //     catch (Exception error)
    //     {
    //         // console.log("error")
    //         // console.log(error)
    //         // res.json({error, message: `Unable to fetch data on mangaVolumeInfo`})
    //         //
    //         return Array.Empty<Volume>();
    //     }
    // }

    
    
    
    
}