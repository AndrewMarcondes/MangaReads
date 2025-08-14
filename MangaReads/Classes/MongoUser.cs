using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangaReads.Classes;

public class MongoUser
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    public string name { get; set; } = string.Empty;
    
    [BsonElement("mangas")]
    public List<MongoUserManga> mangas { get; set; } = new List<MongoUserManga>();

    public User ToUser()
    {
        return new User
        {
            name = this.name,
            mangas = this.mangas.Select(m => m.ToUserManga()).ToList()
        };
    }

    public static MongoUser FromUser(User user)
    {
        return new MongoUser
        {
            name = user.name,
            mangas = user.mangas.Select(MongoUserManga.FromUserManga).ToList()
        };
    }
}

public class MongoUserManga
{
    [BsonElement("name")]
    public string name { get; set; } = string.Empty;
    
    [BsonElement("status")]
    public string status { get; set; } = string.Empty;
    
    [BsonElement("volume")]
    public string volume { get; set; } = string.Empty;
    
    [BsonElement("title")]
    public string title { get; set; } = string.Empty;
    
    [BsonElement("description")]
    public string description { get; set; } = string.Empty;
    
    [BsonElement("releaseData")]
    public int releaseData { get; set; }
    
    [BsonElement("image")]
    public string image { get; set; } = string.Empty;
    
    [BsonElement("volumeData")]
    public List<MongoVolume> volumeData { get; set; } = new List<MongoVolume>();
    
    [BsonElement("thirdPartyId")]
    public string thirdPartyId { get; set; } = string.Empty;

    public UserManga ToUserManga()
    {
        return new UserManga
        {
            name = this.name,
            status = this.status,
            volume = this.volume,
            title = this.title,
            description = this.description,
            releaseData = this.releaseData,
            image = this.image,
            volumeData = this.volumeData.Select(v => v.ToVolume()).ToList(),
            thirdPartyId = this.thirdPartyId
        };
    }

    public static MongoUserManga FromUserManga(UserManga userManga)
    {
        return new MongoUserManga
        {
            name = userManga.name,
            status = userManga.status,
            volume = userManga.volume,
            title = userManga.title,
            description = userManga.description,
            releaseData = userManga.releaseData,
            image = userManga.image,
            volumeData = userManga.volumeData.Select(MongoVolume.FromVolume).ToList(),
            thirdPartyId = userManga.thirdPartyId
        };
    }
}