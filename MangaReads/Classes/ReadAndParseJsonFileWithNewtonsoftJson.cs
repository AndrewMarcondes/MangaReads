using Newtonsoft.Json;

namespace MangaReads.Classes;

public class ReadAndParseJsonFileWithNewtonsoftJson
{
    private readonly string _sampleJsonFilePath;

    public ReadAndParseJsonFileWithNewtonsoftJson(string sampleJsonFilePath)
    {
        _sampleJsonFilePath = sampleJsonFilePath;
    }
    
    public List<Manga> UseUserDefinedObjectWithNewtonsoftJson()
    {
        using StreamReader reader = new(_sampleJsonFilePath);
        var json = reader.ReadToEnd();
        List<Manga> mangas = JsonConvert.DeserializeObject<List<Manga>>(json);
        return mangas;
    }
}