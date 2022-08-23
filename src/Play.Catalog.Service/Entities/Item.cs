using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Play.Catalog.Service.Entities
{

    public class Item
    {

        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        [BsonRepresentation(BsonType.String)]
        public DateTimeOffset CreatedDate { get; set; }


    }

}