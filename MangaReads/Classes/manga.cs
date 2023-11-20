namespace MangaReads.Classes;

public class Manga
{
    public string title { get; set; }
    public string description { get; set; }
    public int releaseData { get; set; }
    public string image { get; set; }
    public List<Volume> volumes { get; set; }
}

public class Volume
{
    public string id { get; set; }
    public int volumeNumber { get; set; }
    public string fileName { get; set; }
}

