using GlassDataBas.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GlassDataBas.Services;

public class GlassShopService
{
    private readonly IMongoCollection<Glass> _glassCollection;


    //Koppla oss mot databasen
    public GlassShopService(
       IOptions<GlassShopDatabasSettings> glassDatabasSettings)
    {
        var mongoClient = new MongoClient(glassDatabasSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(glassDatabasSettings.Value.DatabaseName);

        _glassCollection = mongoDatabase.GetCollection<Glass>(glassDatabasSettings.Value.GlassCollectionName);
    }



    public async Task<List<Glass>> GetAsync() =>
        await _glassCollection.Find(_ => true).ToListAsync();

    public async Task<Glass?> GetAsync(string id) =>
        await _glassCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Glass newGlass) =>
        await _glassCollection.InsertOneAsync(newGlass);

    public async Task UpdateAsync(string id, Glass updatedGlass) =>
        await _glassCollection.ReplaceOneAsync(x => x.Id == id, updatedGlass);

    public async Task RemoveAsync(string id) =>
        await _glassCollection.DeleteOneAsync(x => x.Id == id);
}
