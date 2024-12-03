using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spongebob.Models
{
    public class Newsletter
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        required public string Email { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
