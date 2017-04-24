using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using GrubBuddy.Models;
using GrubBuddy.DataAccess.Interfaces;

namespace GrubBuddy.DataAccess
{
    public class GrubsRepository : IGrubsRepository
    {
        private readonly IMongoDatabase _db;
        public GrubsRepository(string connectionString, string dbName)
        {
            _db = new MongoClient(connectionString).GetDatabase(dbName);
        }

        public IEnumerable<Grub> Get() {
            _db.GetCollection<Grub>("Grubs").Find(x => true).ToList();
            return _db.GetCollection<Grub>("Grubs").Find(x => true).ToList();
        }
        
        public IEnumerable<Grub> GetByName(string name) {
            var filter = Builders<Grub>.Filter.Regex("CreatorName",  BsonRegularExpression.Create(new Regex(name, RegexOptions.IgnoreCase)));
            return _db.GetCollection<Grub>("Grubs").FindAsync<Grub>(filter).Result.ToList();
        }

        public async Task<Grub> Insert(Grub grub)
        {
            grub.GrubId = Guid.NewGuid();
            await _db.GetCollection<Grub>("Grubs").InsertOneAsync(grub);

            return grub;
        }
    }
}


