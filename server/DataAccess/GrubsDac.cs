using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using GrubBuddy.Models;

namespace GrubBuddy.DataAccess
{
    public interface IGrubsDac {
        IEnumerable<Grubs> Get();
        IEnumerable<Grubs> GetByName(string name);
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

        public IEnumerable<Grubs> Get() {
            return _db.GetCollection<Grubs>("Grubs").Find(x => true).ToList();
        }
        
        public IEnumerable<Grubs> GetByName(string name) {
            var filter = Builders<Grubs>.Filter.Regex("CreatorName",  BsonRegularExpression.Create(new Regex(name, RegexOptions.IgnoreCase)));
            return _db.GetCollection<Grubs>("Grubs").FindAsync<Grubs>(filter).Result.ToList();
        }
    }
}


