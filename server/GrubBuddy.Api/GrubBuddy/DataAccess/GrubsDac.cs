using System.Collections.Generic;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using GrubBuddy.Models;

namespace GrubBuddy.DataAccess
{
    public interface IGrubsDac {
        IEnumerable<Grub> Get();
        IEnumerable<Grub> GetByName(string name);
    }
    public class GrubsDac : IGrubsDac
    {
        IMongoClient _client;
        IMongoDatabase _db;
        public GrubsDac(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(dbName);   
        }

        public IEnumerable<Grub> Get() {
            var g = _db.GetCollection<Grub>("Grubs").Find(x => true).ToList();
            return _db.GetCollection<Grub>("Grubs").Find(x => true).ToList();
        }
        
        public IEnumerable<Grub> GetByName(string name) {
            var filter = Builders<Grub>.Filter.Regex("CreatorName",  BsonRegularExpression.Create(new Regex(name, RegexOptions.IgnoreCase)));
            return _db.GetCollection<Grub>("Grubs").FindAsync<Grub>(filter).Result.ToList();
        }
    }
}


