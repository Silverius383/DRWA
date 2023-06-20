using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class GuruService
    {
        private readonly IMongoCollection<Guru> _guruCollection;

        public GuruService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _guruCollection = mongoDatabase.GetCollection<Guru>(bookStoreDatabaseSettings.Value.GuruCollectionName);
        }

        public async Task<List<Guru>> GetAsync()
        {
            return await _guruCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Guru> GetAsync(string id)
        {
            return await _guruCollection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Guru guru)
        {
            await _guruCollection.InsertOneAsync(guru);
        }

        public async Task UpdateAsync(string id, Guru guruIn)
        {
            await _guruCollection.ReplaceOneAsync(g => g.Id == id, guruIn);
        }

        public async Task DeleteAsync(string id)
        {
            await _guruCollection.DeleteOneAsync(g => g.Id == id);
        }
    }
}
