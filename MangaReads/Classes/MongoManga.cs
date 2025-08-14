using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MangaReads.Classes;

public class MongoManga
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
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

    public Manga ToManga()
    {
        return new Manga
        {
            title = this.title,
            description = this.description,
            releaseData = this.releaseData,
            image = this.image,
            volumeData = this.volumeData.Select(v => v.ToVolume()).ToList(),
            thirdPartyId = this.thirdPartyId
        };
    }

    public static MongoManga FromManga(Manga manga)
    {
        return new MongoManga
        {
            title = manga.title,
            description = manga.description,
            releaseData = manga.releaseData,
            image = manga.image,
            volumeData = manga.volumeData.Select(MongoVolume.FromVolume).ToList(),
            thirdPartyId = manga.thirdPartyId
        };
    }
}

public class MongoVolume
{
    [BsonElement("id")]
    public string id { get; set; } = string.Empty;
    
    [BsonElement("volumeNumber")]
    public int volumeNumber { get; set; }
    
    [BsonElement("fileName")]
    public string fileName { get; set; } = string.Empty;

    public Volume ToVolume()
    {
        return new Volume
        {
            id = this.id,
            volumeNumber = this.volumeNumber,
            fileName = this.fileName
        };
    }

    public static MongoVolume FromVolume(Volume volume)
    {
        return new MongoVolume
        {
            id = volume.id,
            volumeNumber = volume.volumeNumber,
            fileName = volume.fileName
        };
    }
}