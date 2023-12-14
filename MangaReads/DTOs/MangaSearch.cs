using System.Text.Json.Nodes;

namespace MangaReads.DTOs;

public class SearchResult
{
    public string result { get; set; }
    public string response { get; set; }
    public List<MangaSearch> data { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
}

public class MangaSearch
{
    public string id { get; set; }
    public string type { get; set; }
    public Attributes attributes { get; set; }
}

public class InformationResult
{
    public string result { get; set; }
    public string response { get; set; }
    public MangaSearch data { get; set; }
}

public class Attributes
{
    public EnglishObject title { get; set; }
    public List<object> altTitles { get; set; }
    public EnglishObject description { get; set; }
    public string status { get; set; }
    // public string year { get; set; }
    public List<object> tags { get; set; }
}

public class EnglishObject
{
    public string en { get; set; }
}


public class VolumeResult
{
    public string result { get; set; }
    public JsonObject volumes { get; set; }
}

public class Volume
{
    public string volume { get; set; }
    public string count { get; set; }
    //public List<Chapter> chapters { get; set; }
}

public class Chapter
{
    public string chapter { get; set; }
    public string id { get; set; }
    public List<string> others { get; set; }
    public string count { get; set; }
}

