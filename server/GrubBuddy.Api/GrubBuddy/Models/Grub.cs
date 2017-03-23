using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using GrubBuddy.Enums;

namespace GrubBuddy.Models
{
    public class Grub {
        [BsonElement("grubId")]
        public Guid? Id {get; set;}  
        
        public long UserId { get; set; }

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