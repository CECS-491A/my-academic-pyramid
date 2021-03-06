﻿using MongoDB.Bson;
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

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Action")]
        public string Action { get; set; }

        [BsonElement("User IP Address")]
        public string UserIPAddress { get; set; }

        [BsonElement("User Location")]
        public string UserLocation { get; set; }

        [BsonElement("Session Duration")]
        public DateTime SessionDuration { get; set; }
    }

    public class RequestLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Request")]
        public string Request { get; set; }
    }
    
    //public class LoginLog
    //{
    //    [BsonId]
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string ID { get; set; }

    //    [BsonElement("Login Date")]
    //    public DateTime Date { get; set; }

    //    [BsonElement("UserName")]
    //    public string UserName { get; set; }

    //    [BsonElement("User IP Address")]
    //    public string UserIPAddress { get; set; }

    //    [BsonElement("User Location")]
    //    public string UserLocation { get; set; }
    //}
}
