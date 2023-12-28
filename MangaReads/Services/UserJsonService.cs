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

        var newJson = new List<User>();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
            {
                userSaved = deserializedUser;
            }
            else
            {
                newJson.Add(deserializedUser);
            }
        }
        
        var manga = new UserManga
        {
            name = mangaName,
            status = "reading",
            volume = "1",
        };
        

        if (userSaved.mangas == null)
        {
            var newMangaList = new List<UserManga>{manga};

            userSaved.mangas = newMangaList;
        }
        else
        {
            userSaved.mangas.Add(manga);
        }
        
        newJson.Add(userSaved);

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }

    public void UpdateUserMangaReadingStatus(string userName, string mangaName, string status)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var userSaved = new User();

        var newJson = new List<User>();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
            {
                userSaved = deserializedUser;
            }
            else
            {
                newJson.Add(deserializedUser);
            }
        }

        var newMangaList = new List<UserManga> { };
        
        foreach (var manga in userSaved.mangas)
        {
            if (manga.name == mangaName)
            {
                manga.status = status;
                newMangaList.Add(manga);
            }
            else
            {
                newMangaList.Add(manga);
            }
        }

        userSaved.mangas = newMangaList;
        
        newJson.Add(userSaved);

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }

    public void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var userSaved = new User();

        var newJson = new List<User>();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
            {
                userSaved = deserializedUser;
            }
            else
            {
                newJson.Add(deserializedUser);
            }
        }

        var newMangaList = new List<UserManga> { };
        
        foreach (var manga in userSaved.mangas)
        {
            if (manga.name == mangaName)
            {
                manga.volume = newVolumeNumber;
                newMangaList.Add(manga);
            }
            else
            {
                newMangaList.Add(manga);
            }
        }

        userSaved.mangas = newMangaList;
        
        newJson.Add(userSaved);

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }

    public void DeleteUser(string userName)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var newJson = new List<User>();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name != userName)
            {
                newJson.Add(deserializedUser);
            }
        }
        
        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }

    public void DeleteUserManga(string userName, string mangaName)
    {
        var userJson = new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").ReadFromJson();
        
        var userSaved = new User();

        var newJson = new List<User>();
        
        foreach (var user in userJson)
        {
            var deserializedUser = JsonConvert.DeserializeObject<User>(user.ToString());

            if (deserializedUser.name == userName)
            {
                userSaved = deserializedUser;
            }
            else
            {
                newJson.Add(deserializedUser);
            }
        }

        var newMangaList = new List<UserManga> { };
        
        foreach (var manga in userSaved.mangas)
        {
            if (manga.name != mangaName)
                newMangaList.Add(manga);
        }

        userSaved.mangas = newMangaList;
        
        newJson.Add(userSaved);

        new ReadAndParseJsonFileWithNewtonsoftJson("userData.json").WriteToJson(newJson);
    }
    
}