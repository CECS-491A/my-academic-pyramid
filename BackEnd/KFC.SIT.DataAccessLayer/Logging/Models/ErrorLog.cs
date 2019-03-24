using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Models
{
    public class ErrorLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("Date")]
        public string Date { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonElement("Target Site")]
        public string TargetSite { get; set; }

        [BsonElement("Line of Code")]
        public string LineOfCode { get; set; }

        [BsonElement("User")]
        public string User { get; set; }

        [BsonElement("Request")]
        public string Request { get; set; }
    }
}




