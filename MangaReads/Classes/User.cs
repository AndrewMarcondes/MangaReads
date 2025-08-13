using System.ComponentModel.DataAnnotations;

namespace MangaReads.Classes;

public class User
{
    [Key]
    public int Id { get; set; }
    public string name { get; set; }
    public List<UserManga> mangas { get; set; } = new List<UserManga>();
}

public class UserManga
{
    [Key]
    public int Id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string volume { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    // Manga properties copied from Manga class for composition instead of inheritance
    public string title { get; set; }
    public string description { get; set; }
    public int releaseData { get; set; }
    public string image { get; set; }
    public List<Volume> volumeData { get; set; } = new List<Volume>();
    public string thirdPartyId { get; set; }
}