using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Services
{
    public class MapelService
    {
        private readonly IMongoCollection<Mapel> _mapelCollection;

        public MapelService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _mapelCollection = mongoDatabase.GetCollection<Mapel>(bookStoreDatabaseSettings.Value.MapelCollectionName);
        }

        public async Task<List<Mapel>> GetAsync() =>
            await _mapelCollection.Find(_ => true).ToListAsync();

        public async Task<Mapel> GetAsync(string id) =>
            await _mapelCollection.Find(mapel => mapel.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Mapel mapel) =>
            await _mapelCollection.InsertOneAsync(mapel);

        public async Task UpdateAsync(string id, Mapel updatedMapel) =>
            await _mapelCollection.ReplaceOneAsync(mapel => mapel.Id == id, updatedMapel);

        public async Task DeleteAsync(string id) =>
            await _mapelCollection.DeleteOneAsync(mapel => mapel.Id == id);
    }
}
