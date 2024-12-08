using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Spongebob.Models
{
    public class ContactMessage
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public string EmailAddress { set; get; } = string.Empty;
        
        [BsonElement("name")]
        public string Name { set; get; } = string.Empty;
        
        [BsonElement("number")]
        public string PhoneNumber { set; get; } = string.Empty;
        
        [BsonElement("message")]
        public string Message { set; get; } = string.Empty;

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}