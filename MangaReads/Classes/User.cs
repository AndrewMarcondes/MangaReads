namespace MangaReads.Classes;

public class User
{
    public string name { get; set; }
    private List<UserManga> mangas { get; set; }
    
}

public class UserManga
{
    public string name { get; set; }
    public string status { get; set; }
    public int volumeNumber { get; set; }
}