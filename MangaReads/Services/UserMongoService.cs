using MangaReads.Classes;
using MangaReads.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace MangaReads.Services;

public class UserMongoService : IUserService
{
    private readonly IMongoCollection<MongoUser> _userCollection;

    public UserMongoService(IOptions<MongoDbSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<MongoUser>("users");
    }

    public User GetUser(string userName)
    {
        var mongoUser = _userCollection.Find(u => u.name == userName).FirstOrDefault();
        return mongoUser?.ToUser() ?? new User { name = userName };
    }

    public async Task<User?> GetUserAsync(string userName)
    {
        var mongoUser = await _userCollection.Find(u => u.name == userName).FirstOrDefaultAsync();
        return mongoUser?.ToUser();
    }

    public void CreateUser(string userName)
    {
        var existingUser = _userCollection.Find(u => u.name == userName).FirstOrDefault();
        if (existingUser == null)
        {
            var newUser = new MongoUser { name = userName };
            _userCollection.InsertOne(newUser);
        }
    }

    public async Task<string> CreateUserAsync(string userName)
    {
        var existingUser = await _userCollection.Find(u => u.name == userName).FirstOrDefaultAsync();
        if (existingUser == null)
        {
            var newUser = new MongoUser { name = userName };
            await _userCollection.InsertOneAsync(newUser);
            return newUser.Id ?? string.Empty;
        }
        return existingUser.Id ?? string.Empty;
    }

    public void AddUserManga(string userName, string mangaName)
    {
        var filter = Builders<MongoUser>.Filter.Eq(u => u.name, userName);
        var update = Builders<MongoUser>.Update.Push(u => u.mangas, new MongoUserManga { name = mangaName });
        _userCollection.UpdateOne(filter, update);
    }

    public async Task AddUserMangaAsync(string userName, string mangaName)
    {
        var filter = Builders<MongoUser>.Filter.Eq(u => u.name, userName);
        var update = Builders<MongoUser>.Update.Push(u => u.mangas, new MongoUserManga { name = mangaName });
        await _userCollection.UpdateOneAsync(filter, update);
    }

    public void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber)
    {
        var filter = Builders<MongoUser>.Filter.And(
            Builders<MongoUser>.Filter.Eq(u => u.name, userName),
            Builders<MongoUser>.Filter.ElemMatch(u => u.mangas, m => m.name == mangaName)
        );
        var update = Builders<MongoUser>.Update.Set("mangas.$.volume", newVolumeNumber);
        _userCollection.UpdateOne(filter, update);
    }

    public async Task UpdateUserMangaVolumeNumberAsync(string userName, string mangaName, string newVolumeNumber)
    {
        var filter = Builders<MongoUser>.Filter.And(
            Builders<MongoUser>.Filter.Eq(u => u.name, userName),
            Builders<MongoUser>.Filter.ElemMatch(u => u.mangas, m => m.name == mangaName)
        );
        var update = Builders<MongoUser>.Update.Set("mangas.$.volume", newVolumeNumber);
        await _userCollection.UpdateOneAsync(filter, update);
    }

    public void UpdateUserMangaReadingStatus(string userName, string mangaName, string status)
    {
        var filter = Builders<MongoUser>.Filter.And(
            Builders<MongoUser>.Filter.Eq(u => u.name, userName),
            Builders<MongoUser>.Filter.ElemMatch(u => u.mangas, m => m.name == mangaName)
        );
        var update = Builders<MongoUser>.Update.Set("mangas.$.status", status);
        _userCollection.UpdateOne(filter, update);
    }

    public async Task UpdateUserMangaReadingStatusAsync(string userName, string mangaName, string status)
    {
        var filter = Builders<MongoUser>.Filter.And(
            Builders<MongoUser>.Filter.Eq(u => u.name, userName),
            Builders<MongoUser>.Filter.ElemMatch(u => u.mangas, m => m.name == mangaName)
        );
        var update = Builders<MongoUser>.Update.Set("mangas.$.status", status);
        await _userCollection.UpdateOneAsync(filter, update);
    }

    public void DeleteUserManga(string userName, string mangaName)
    {
        var filter = Builders<MongoUser>.Filter.Eq(u => u.name, userName);
        var update = Builders<MongoUser>.Update.PullFilter(u => u.mangas, m => m.name == mangaName);
        _userCollection.UpdateOne(filter, update);
    }

    public async Task DeleteUserMangaAsync(string userName, string mangaName)
    {
        var filter = Builders<MongoUser>.Filter.Eq(u => u.name, userName);
        var update = Builders<MongoUser>.Update.PullFilter(u => u.mangas, m => m.name == mangaName);
        await _userCollection.UpdateOneAsync(filter, update);
    }

    public void DeleteUser(string userName)
    {
        _userCollection.DeleteOne(u => u.name == userName);
    }

    public async Task<bool> DeleteUserAsync(string userName)
    {
        var result = await _userCollection.DeleteOneAsync(u => u.name == userName);
        return result.DeletedCount > 0;
    }

    public void GetUserManga(string userName)
    {
        // This method seems to be a placeholder in the interface
        // Implementation would depend on what it's supposed to return
        var user = GetUser(userName);
        // Could return user.mangas or perform some other operation
    }

    public async Task<List<UserManga>> GetUserMangaAsync(string userName)
    {
        var user = await GetUserAsync(userName);
        return user?.mangas ?? new List<UserManga>();
    }
}