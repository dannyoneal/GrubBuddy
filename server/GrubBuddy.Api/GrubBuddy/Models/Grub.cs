using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GrubBuddy.Models
{
    public class Grub {
        [BsonElement("id")]
        public ObjectId Id {get; set;}    

        [BsonElement("creatorName")]
        public string CreatorName {get; set;}

        [BsonElement("location")]
        public string Location {get; set;}

        [BsonElement("createdDateUtc")]
        public string createdDateUtc {get; set;}

        [BsonElement("grubTimeUtc")]
        public string GrubTimeUtc {get; set;}

        [BsonElementAttribute("transportationMethodId")]
        public int TransportationMethodId {get; set;}

        [JsonConverter(typeof(StringEnumConverter))]
        public Transportation TransportationMethod {
            get {
                    return (Transportation) TransportationMethodId;
                }
            
            set {
                    TransportationMethodId = (int) value;
                }
        }
    }
}