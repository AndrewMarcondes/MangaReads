using System.ComponentModel.DataAnnotations;

namespace MangaReads.Classes;

public class Manga
{
    [Key]
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int releaseData { get; set; }
    public string image { get; set; }
    public List<Volume> volumeData { get; set; } = new List<Volume>();
    public string thirdPartyId { get; set; }
}

public class Volume
{
    [Key]
    public int Id { get; set; }
    public string id { get; set; }
    public int volumeNumber { get; set; }
    public string fileName { get; set; }
    public int MangaId { get; set; }
    public Manga Manga { get; set; }
}

