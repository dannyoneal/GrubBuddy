using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrubBuddy.Models
{
    public class Grubs {
        public ObjectId Id {get; set;}
        
        [BsonElement("creatorName")]
        public string CreatorName {get; set;}
        [BsonElement("location")]
        public string Location {get; set;}
        
    }
}