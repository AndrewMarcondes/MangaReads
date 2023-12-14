using MangaReads.Classes;
using MangaReads.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface IMangaService
{
    Task<List<MangaSearch>> MangaSearch(string mangaName);
    Task<MangaSearch> GetMangaInformation(string mangaId);
    Task<List<DTOs.Volume>> GetMangaVolume(string mangaId);
}