using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface IMangaService
{
    Task<string> MangaSearch(string mangaName);
    string GetMangaInformation(string mangaName);
}