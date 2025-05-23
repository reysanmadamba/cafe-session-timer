using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;


namespace cafe_session_timer.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string PasswordHash { get; set; }

        [BsonElement("timeRemaining")]
        public int TimeRemaining { get; set; }

        [BsonElement("isLocked")]
        public bool isLocked { get; set; }

        [BsonElement("isLoggedIn")]
        public bool isLoggedIn { get; set; }

    }
}
