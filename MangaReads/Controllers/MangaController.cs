using Microsoft.AspNetCore.Mvc;
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
    
    [HttpGet("GetMangaSearch")]
    public Task<List<MangaSearch>> GetMangaSearch(string mangaName)
    {
        return _mangaService.MangaSearch(mangaName);
    }
    
    [HttpGet("GetMangaInformation")]
    public Task<MangaSearch> GetMangaInformation(string mangaId)
    {
        return _mangaService.GetMangaInformation(mangaId);
    }
    
    [HttpGet("GetMangaVolumeInformation")]
    public Task<List<Volume>> GetMangaVolumeInformation(string mangaId)
    {
        return _mangaService.GetMangaVolume(mangaId);
    }
    
    
}