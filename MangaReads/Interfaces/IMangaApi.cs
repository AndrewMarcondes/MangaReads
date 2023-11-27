using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface IMangaApi
{
    Task<string> MangaSearch(string mangaName);
    string GetMangaInformation(string mangaName);
}