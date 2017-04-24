using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using GrubBuddy.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GrubBuddy.Models
{
    public class Grub {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid GrubId {get; set;}

        public string UserId { get; set; }

        [BsonElement("creatorName")]
        public string CreatorName {get; set;}

        [BsonElement("location")]
        public string Location {get; set;}

        [BsonElement("grubTimeUtc")]
        public DateTime GrubTimeUtc {get; set;}

        [BsonElement("transportationMethodId")]
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