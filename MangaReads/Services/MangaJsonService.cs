using MangaReads.Classes;
using MangaReads.Controllers;
using MangaReads.Interfaces;
using Newtonsoft.Json;

namespace MangaReads.Services;

public class MangaJsonService : IMangaStorageService
{
    // private readonly ILogger<IMangaStorageService> _logger;
    
    // private readonly IUserService _userService;

    public MangaJsonService()
    {
        // _logger = logger;
        // _userService = userService;
    }

    public List<Manga> GetMangaFromStorage()
    {
        var mangaJson = new ReadAndParseJsonFileWithNewtonsoftJson("mangaData.json").ReadFromJson();
        
        List<Manga> mangaList = new List<Manga>();

	try{

        foreach (var manga in mangaJson)
        {
            var deserializeObject = JsonConvert.DeserializeObject<Manga>(manga.ToString());

            mangaList.Add(deserializeObject);
        }

	}catch(Exception e){


	}

        
        return mangaList;
    }
    
}
