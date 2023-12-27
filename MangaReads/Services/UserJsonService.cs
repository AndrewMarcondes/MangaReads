using MangaReads.Classes;
using MangaReads.Interfaces;

namespace MangaReads.Services;

public class UserJsonService : IUserService
{
    public string GetUser(string userName)
    {

        // This needs to be implemented
        var mangaJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
                 
        var mangaSaved = mangaJson.Find(manga => manga.title == mangaName);
        
        if (mangaSaved != null)
        return mangaSaved;

        throw new NotImplementedException();
    }

    public void UpdateUser(string userName)
    {
        throw new NotImplementedException();
    }

    public void AddUserManga(string userName, string mangaName)
    {
        throw new NotImplementedException();
    }

    public void UpdateUserMangaReadingStatus(string userName, string mangaName)
    {
        throw new NotImplementedException();
    }

    public void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserManga(string userName, string mangaName)
    {
        throw new NotImplementedException();
    }
    
}