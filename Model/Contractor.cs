namespace Jaroszek.ProofOfConcept.MongoDb.Model
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public sealed class Contractor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
    }
}