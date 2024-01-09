using MangaReads.Classes;
using Microsoft.AspNetCore.Mvc;
using MangaReads.DTOs;
using MangaReads.Interfaces;

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
    public async Task<IActionResult> GetMangaSearch(string mangaName)
    {
        return Ok(await _mangaService.MangaSearch(mangaName));
    }
    
    [HttpGet("GetMangaInformation")]
    public async Task<IActionResult> GetMangaInformation(string mangaId)
    {
        return Ok(await _mangaService.GetMangaInformation(mangaId));
    }
    
    [HttpGet("GetMangaVolumeInformation")]
    public async Task<IActionResult> GetMangaVolumeInformation(string mangaId)
    {
        return Ok(await _mangaService.GetMangaVolume(mangaId));
    }
    
    
}