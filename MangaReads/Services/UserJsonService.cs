using MangaReads.Classes;
using MangaReads.Interfaces;
using Newtonsoft.Json;

namespace MangaReads.Services;

public class UserJsonService : IUserService
{
    public User GetUser(string userName)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var userSaved = new User();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
                userSaved = deserializedUser;
        }
        
        return userSaved;
    }

    public void CreateUser(string userName)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var newUser = new User
        {
            name = userName,
        };

        var newJson = userJson.Append(newUser);

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }

    public void AddUserManga(string userName, string mangaName)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var userSaved = new User();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
            {
                userSaved = deserializedUser;
                
            }
        }

        var manga = new UserManga
        {
            name = mangaName,
            status = "reading",
            volume = "1",
        };
        
        userSaved.mangas.Add(manga);
        
        // TODO Remove the old user entry
        
        var newJson = userJson.Append(userSaved);
        

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
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