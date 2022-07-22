using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookStoreAPI.Models
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("price")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("actors")]
        public List<string> Actors { get; set; }
    }

    public class BookFormModel
    {
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("price")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("actors")]
        public List<string> Actors { get; set; }
    }
}
