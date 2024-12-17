using MangaReads.Classes;

namespace MangaReads.Interfaces;

public interface IMangaStorageService
{
    List<Manga> GetMangaFromStorage();

}