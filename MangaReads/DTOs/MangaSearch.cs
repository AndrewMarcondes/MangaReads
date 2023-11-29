namespace MangaReads.DTOs;

public class MangaSearch
{
    public string id { get; set; }
    public string type { get; set; }
    public Attributes attribute { get; set; }
    
}

public class Attributes
{
    public string title { get; set; }
    public List<object> altTitles { get; set; }
    public List<object> description { get; set; }
    public string status { get; set; }
    public int year { get; set; }
    public List<object> tags { get; set; }
}