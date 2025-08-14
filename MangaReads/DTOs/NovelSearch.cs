namespace MangaReads.DTOs;

public class NovelSearchResult
{
    public string result { get; set; }
    public string response { get; set; }
    public List<NovelSearch> data { get; set; }
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
}

public class NovelSearch
{
    public string id { get; set; }
    public string type { get; set; }
    public NovelAttributes attributes { get; set; }
}

public class NovelInformationResult
{
    public string result { get; set; }
    public string response { get; set; }
    public NovelSearch data { get; set; }
}

public class NovelAttributes
{
    public NovelEnglishObject title { get; set; }
    public List<object> altTitles { get; set; }
    public NovelEnglishObject description { get; set; }
    public string status { get; set; }
    public string author { get; set; }
    public string genre { get; set; }
    public List<object> tags { get; set; }
}

public class NovelEnglishObject
{
    public string en { get; set; }
}