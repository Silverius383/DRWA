using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Services
{
    public class KelasService
    {
        private readonly IMongoCollection<Kelas> _kelasCollection;

        public KelasService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
                    var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _kelasCollection = mongoDatabase.GetCollection<Kelas>(
            bookStoreDatabaseSettings.Value.KelasCollectionName);
        }

        public async Task<List<Kelas>> GetAsync() =>
            await _kelasCollection.Find(_ => true).ToListAsync();

        public async Task<Kelas?> GetAsync(string id) =>
            await _kelasCollection.Find(kelas => kelas.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Kelas newKelas) =>
        await _kelasCollection.InsertOneAsync(newKelas);

        public async Task UpdateAsync(string id, Kelas updatedKelas) =>
            await _kelasCollection.ReplaceOneAsync(kelas => kelas.Id == id, updatedKelas);

        public async Task DeleteAsync(string id) =>
            await _kelasCollection.DeleteOneAsync(kelas => kelas.Id == id);
    }
}
