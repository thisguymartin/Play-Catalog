using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Entities
{

    public class Item
    {

        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        [BsonRepresentation(BsonType.String)]
        public DateTimeOffset CreatedDate { get; set; }


    }

}