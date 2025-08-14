using MangaReads.Classes;
using MangaReads.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace MangaReads.Services;

public class MangaMongoService : IMangaStorageService
{
    private readonly IMongoCollection<MongoManga> _mangaCollection;

    public MangaMongoService(IOptions<MongoDbSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _mangaCollection = database.GetCollection<MongoManga>("mangas");
    }

    public List<Manga> GetMangaFromStorage()
    {
        var mongoMangas = _mangaCollection.Find(manga => true).ToList();
        return mongoMangas.Select(m => m.ToManga()).ToList();
    }

    public async Task<List<Manga>> GetMangaFromStorageAsync()
    {
        var mongoMangas = await _mangaCollection.Find(manga => true).ToListAsync();
        return mongoMangas.Select(m => m.ToManga()).ToList();
    }

    public async Task<Manga?> GetMangaByIdAsync(string id)
    {
        var mongoManga = await _mangaCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
        return mongoManga?.ToManga();
    }

    public async Task<Manga?> GetMangaByThirdPartyIdAsync(string thirdPartyId)
    {
        var mongoManga = await _mangaCollection.Find(m => m.thirdPartyId == thirdPartyId).FirstOrDefaultAsync();
        return mongoManga?.ToManga();
    }

    public async Task<string> CreateMangaAsync(Manga manga)
    {
        var mongoManga = MongoManga.FromManga(manga);
        await _mangaCollection.InsertOneAsync(mongoManga);
        return mongoManga.Id ?? string.Empty;
    }

    public async Task<bool> UpdateMangaAsync(string id, Manga manga)
    {
        var mongoManga = MongoManga.FromManga(manga);
        mongoManga.Id = id;
        var result = await _mangaCollection.ReplaceOneAsync(m => m.Id == id, mongoManga);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteMangaAsync(string id)
    {
        var result = await _mangaCollection.DeleteOneAsync(m => m.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<Manga>> SearchMangaAsync(string searchTerm)
    {
        var filter = Builders<MongoManga>.Filter.Or(
            Builders<MongoManga>.Filter.Regex(m => m.title, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
            Builders<MongoManga>.Filter.Regex(m => m.description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
        );

        var mongoMangas = await _mangaCollection.Find(filter).ToListAsync();
        return mongoMangas.Select(m => m.ToManga()).ToList();
    }
}