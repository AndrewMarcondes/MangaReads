using MangaReads.Classes;

namespace MangaReads.Interfaces;

public interface INovelStorageService
{
    List<Novel> GetNovelFromStorage();
}