using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using GrubBuddy.Models;

namespace GrubBuddy.Controllers
{
    public class GrubsController : Controller
    {
        IMongoClient _client;
        IMongoDatabase _db;
        public GrubsController()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("GrubsDB");   
        }

        [HttpGet]
        public IEnumerable<Grubs> Get() {
            return _db.GetCollection<Grubs>("grubs").Find(x => true).ToList();
        }
        [HttpGet]
        public IEnumerable<Grubs> GetByName(string name) {
            var filter = Builders<Grubs>.Filter.Regex("CreatorName",  BsonRegularExpression.Create(new Regex(name, RegexOptions.IgnoreCase)));
            return _db.GetCollection<Grubs>("grubs").FindAsync<Grubs>(filter).Result.ToList();
        }
    }
}


