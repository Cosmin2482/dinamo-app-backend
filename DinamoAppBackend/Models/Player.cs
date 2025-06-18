using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DinamoAppBackend.Models
{
    public class Player
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nume { get; set; }
        public string Imagine { get; set; } // URL sau base64
        public DateTime ZiDeNastere { get; set; }
        public string Pozitie { get; set; }
        public string Nationalitate { get; set; }
        public string Descriere { get; set; }
        public int NumarTricou { get; set; }
        public string PiciorPreferat { get; set; } // ex: "Stâng", "Drept", "Ambele"
    }
}
