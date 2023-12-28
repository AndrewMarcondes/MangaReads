using MangaReads.Classes;

namespace MangaReads.Interfaces;

public interface IUserService
{
    User GetUser(string userName);
    void CreateUser(string userName);
    void AddUserManga(string userName, string mangaName);
    void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber);
    void UpdateUserMangaReadingStatus(string userName, string mangaName, string status);
    void DeleteUserManga(string userName, string mangaName);
    void DeleteUser(string userName);
}