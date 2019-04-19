using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DataAccessLayer.Models
{
    public class TelemetryLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Year")]
        public int Year { get; set; }

        [BsonElement("Month")]
        public int Month { get; set; }

        [BsonElement("Action")]
        public string Action { get; set; }

        [BsonElement("User IP Address")]
        public string UserIPAddress { get; set; }
 

        [BsonElement("User Location")]
        public string UserLocation { get; set; }
    }
}
