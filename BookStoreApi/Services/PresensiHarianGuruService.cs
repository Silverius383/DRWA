using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class PresensiHarianGuruService
    {
        private readonly IMongoCollection<PresensiHarianGuru> _presensiHarianGuruCollection;

        public PresensiHarianGuruService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _presensiHarianGuruCollection = mongoDatabase.GetCollection<PresensiHarianGuru>(bookStoreDatabaseSettings.Value.PresensiHarianGuruCollectionName);
        }

        public async Task<List<PresensiHarianGuru>> GetAsync()
        {
            return await _presensiHarianGuruCollection.Find(_ => true).ToListAsync();
        }

        public async Task<PresensiHarianGuru> GetAsync(string id)
        {
            return await _presensiHarianGuruCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PresensiHarianGuru presensi)
        {
            await _presensiHarianGuruCollection.InsertOneAsync(presensi);
        }

        public async Task UpdateAsync(string id, PresensiHarianGuru presensiIn)
        {
            await _presensiHarianGuruCollection.ReplaceOneAsync(p => p.Id == id, presensiIn);
        }

        public async Task DeleteAsync(string id)
        {
            await _presensiHarianGuruCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
