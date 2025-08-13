using MangaReads.Classes;
using MangaReads.Data;
using MangaReads.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MangaReads.Services;

public class UserPostgreSqlService : IUserService
{
    private readonly MangaReadsDbContext _context;
    private readonly IMangaStorageService _mangaStorageService;

    public UserPostgreSqlService(MangaReadsDbContext context, IMangaStorageService mangaStorageService)
    {
        _context = context;
        _mangaStorageService = mangaStorageService;
    }

    public User GetUser(string userName)
    {
        var user = _context.Users
            .Include(u => u.mangas)
            .FirstOrDefault(u => u.name == userName);

        if (user == null)
            return new User();

        var mangaFromStorage = _mangaStorageService.GetMangaFromStorage();
        var newMangaList = new List<UserManga>();

        if (mangaFromStorage.Count != 0)
        {
            foreach (var manga in user.mangas)
            {
                foreach (var mangaData in mangaFromStorage)
                {
                    if (manga.name.ToLower() == mangaData.title.ToLower())
                    {
                        UserManga consolidated = new UserManga()
                        {
                            name = manga.name,
                            status = manga.status,
                            volume = manga.volume,
                            title = mangaData.title,
                            description = mangaData.description,
                            releaseData = mangaData.releaseData,
                            image = mangaData.image,
                            volumeData = mangaData.volumeData,
                            thirdPartyId = mangaData.thirdPartyId
                        };

                        newMangaList.Add(consolidated);
                    }
                }
            }
        }

        user.mangas = newMangaList;
        return user;
    }

    public void CreateUser(string userName)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.name == userName);
        if (existingUser != null)
            return;

        var newUser = new User
        {
            name = userName,
            mangas = new List<UserManga>()
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public void AddUserManga(string userName, string mangaName)
    {
        var user = _context.Users
            .Include(u => u.mangas)
            .FirstOrDefault(u => u.name == userName);

        if (user == null)
            return;

        var manga = new UserManga
        {
            name = mangaName,
            status = "reading",
            volume = "1",
            UserId = user.Id
        };

        if (user.mangas == null)
            user.mangas = new List<UserManga>();

        user.mangas.Add(manga);
        _context.SaveChanges();
    }

    public void UpdateUserMangaReadingStatus(string userName, string mangaName, string status)
    {
        var user = _context.Users
            .Include(u => u.mangas)
            .FirstOrDefault(u => u.name == userName);

        if (user?.mangas == null)
            return;

        var manga = user.mangas.FirstOrDefault(m => m.name == mangaName);
        if (manga != null)
        {
            manga.status = status;
            _context.SaveChanges();
        }
    }

    public void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber)
    {
        var user = _context.Users
            .Include(u => u.mangas)
            .FirstOrDefault(u => u.name == userName);

        if (user?.mangas == null)
            return;

        var manga = user.mangas.FirstOrDefault(m => m.name == mangaName);
        if (manga != null)
        {
            manga.volume = newVolumeNumber;
            _context.SaveChanges();
        }
    }

    public void DeleteUserManga(string userName, string mangaName)
    {
        var user = _context.Users
            .Include(u => u.mangas)
            .FirstOrDefault(u => u.name == userName);

        if (user?.mangas == null)
            return;

        var manga = user.mangas.FirstOrDefault(m => m.name == mangaName);
        if (manga != null)
        {
            user.mangas.Remove(manga);
            _context.SaveChanges();
        }
    }

    public void DeleteUser(string userName)
    {
        var user = _context.Users.FirstOrDefault(u => u.name == userName);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public void GetUserManga(string userName)
    {
        throw new NotImplementedException();
    }
}