using MangaReads.Classes;
using MangaReads.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface IMangaService
{
    Task<List<MangaSearch>> MangaSearch(string mangaName);
    Task<Manga> GetMangaInformation(string mangaName);
}