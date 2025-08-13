using MangaReads.Classes;
using MangaReads.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MangaReads.Interfaces;

public interface INovelService
{
    Task<List<Novel>> NovelSearch(string novelName);
    Task<Novel> GetNovelInformation(string novelId);
}