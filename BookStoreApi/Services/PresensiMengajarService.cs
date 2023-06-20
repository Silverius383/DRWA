using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services
{
    public class PresensiMengajarService
    {
        private readonly IMongoCollection<PresensiMengajar> _presensiMengajarCollection;

        public PresensiMengajarService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _presensiMengajarCollection = mongoDatabase.GetCollection<PresensiMengajar>(bookStoreDatabaseSettings.Value.PresensiMengajarCollectionName);
        }

        public async Task<List<PresensiMengajar>> GetAsync()
        {
            return await _presensiMengajarCollection.Find(_ => true).ToListAsync();
        }

        public async Task<PresensiMengajar> GetAsync(string id)
        {
            return await _presensiMengajarCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PresensiMengajar presensi)
        {
            await _presensiMengajarCollection.InsertOneAsync(presensi);
        }

        public async Task UpdateAsync(string id, PresensiMengajar presensiIn)
        {
            await _presensiMengajarCollection.ReplaceOneAsync(p => p.Id == id, presensiIn);
        }

        public async Task DeleteAsync(string id)
        {
            await _presensiMengajarCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}
