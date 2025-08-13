using System.ComponentModel.DataAnnotations;

namespace MangaReads.Classes;

public class Novel
{
    [Key]
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string author { get; set; }
    public string genre { get; set; }
    public int releaseData { get; set; }
    public string image { get; set; }
    public string thirdPartyId { get; set; }
}