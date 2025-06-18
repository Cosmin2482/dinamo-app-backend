using DinamoAppBackend.Models;
using DinamoAppBackend.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DinamoAppBackend.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<Player> _players;

        public PlayerService(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _players = database.GetCollection<Player>("Players");
        }

        public async Task<List<Player>> GetAllAsync() =>
            await _players.Find(player => true).ToListAsync();

        public async Task<Player> GetByIdAsync(string id) =>
            await _players.Find(player => player.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Player player) =>
            await _players.InsertOneAsync(player);

        public async Task UpdateAsync(string id, Player updatedPlayer) =>
            await _players.ReplaceOneAsync(player => player.Id == id, updatedPlayer);

        public async Task DeleteAsync(string id) =>
            await _players.DeleteOneAsync(player => player.Id == id);
    }
}
