using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDB.DataContracts
{
    public class TicketMongo
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid TicketId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set;}
        public DateTime? LastUpdate { get; set;}
        public bool Open { get; set; }

    }
}
