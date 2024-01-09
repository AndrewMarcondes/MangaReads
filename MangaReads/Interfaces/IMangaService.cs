using MangaReads.Classes;
using MangaReads.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface IMangaService
{
    Task<List<Manga>> MangaSearch(string mangaName);
    Task<Manga> GetMangaInformation(string mangaId);
    Task<List<Classes.Volume>> GetMangaVolume(string mangaId);
}