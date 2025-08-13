using MangaReads.Classes;
using MangaReads.Data;
using MangaReads.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MangaReads.Services;

public class MangaPostgreSqlService : IMangaStorageService
{
    private readonly MangaReadsDbContext _context;

    public MangaPostgreSqlService(MangaReadsDbContext context)
    {
        _context = context;
    }

    public List<Manga> GetMangaFromStorage()
    {
        return _context.Mangas
            .Include(m => m.volumeData)
            .ToList();
    }
}